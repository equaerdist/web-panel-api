using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using web_panel_api.Dto;
using web_panel_api.Models;
using web_panel_api.Services;

namespace web_panel_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public StatisticsController(IMapper mapper) { _mapper = mapper; }

        [HttpPost]
        public async Task<GetStaticticsDto> GetStats(DateDto dates, string group)
        {
            var ctx = new clientContext();
            var result = new GetStaticticsDto();
            var temporary = await ctx.Users.Where(u => u.CreatedAt <= dates.LastTime
            && u.CreatedAt >= dates.FirstTime).ToListAsync();
            foreach (var user in temporary)
            {
                switch (group)
                {
                    case "day":
                            user.CreatedAt = DateGrouper.GroupByDay(user.CreatedAt); break;
                    case "month":
                            user.CreatedAt = DateGrouper.GroupByMonth(user.CreatedAt); break;
                    case "year":
                        user.CreatedAt = DateGrouper.GroupByYear(user.CreatedAt); break;
                    default:
                        throw new ArgumentException();
                }
            }
            result.AmountOfCreatedUsers = temporary
                .GroupBy(u => u.CreatedAt).Select(g => new DatePoint(g.Key, g.Count())).OrderBy(dt => dt.Time);
            var temp = ctx.PayHistories
               .Where(ph => ph.PaidAt != null && ph.PaidAt <= dates.LastTime && ph.PaidAt >= dates.FirstTime && ph.PaymentMethod == "tariff")
               .AsEnumerable()
              .GroupBy(ph => ph.Currency);

            foreach(var currencyGroup in temp)
            {
                foreach(var transaction in currencyGroup)
                {
                    switch(group)
                    {
                        case "day":
                                transaction.PaidAt = DateGrouper.GroupByDay((DateTime)transaction.PaidAt); break;
                        case "month":
                            transaction.PaidAt = DateGrouper.GroupByMonth((DateTime)transaction.PaidAt); break;
                        case "year":
                            transaction.PaidAt = DateGrouper.GroupByYear((DateTime)transaction.PaidAt); break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
            var tempDictionary = new Dictionary<string, IEnumerable<DatePoint>>();
            foreach(var currencyGroup in temp)
            {
                var datePointList = new List<DatePoint>();
                var temporaryGroup = currencyGroup.AsEnumerable().GroupBy(ph => ph.PaidAt);
                foreach(var dateGroup in temporaryGroup)
                {
                    int datePointAmount = dateGroup.AsEnumerable().DistinctBy(ph => ph.UserId).Count();
                    var datePoint = new DatePoint((DateTime)dateGroup.Key, datePointAmount);
                    datePointList.Add(datePoint);
                }
                
                tempDictionary[currencyGroup.Key] = datePointList.OrderBy(dt => dt.Time);
            }
            if (tempDictionary.ContainsKey("rub"))
                result.AmountOfUsersWhoPayRub = tempDictionary["rub"];
            if (tempDictionary.ContainsKey("del"))
                result.AmountOfUsersWhoPayDel = tempDictionary["del"];
            if (tempDictionary.ContainsKey("usdt"))
                result.AmountOfUsersWhoPayUsdt = tempDictionary["usdt"];
            if (tempDictionary.ContainsKey("ton"))
                result.AmountOfUsersWhoPayTon = tempDictionary["ton"];
            return result;
        }

    }
}
