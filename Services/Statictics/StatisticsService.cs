using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks.Dataflow;
using web_panel_api.Dto;
using web_panel_api.Models;

namespace web_panel_api.Services.Statictics
{
    public class StatisticsService : IStatisticsService
    {
        private static void MapDataFromPaidAmountDictionary(GetStaticticsDto result, Dictionary<string, IEnumerable<DatePoint>> tempDictionary)
        {
            if (tempDictionary.ContainsKey("rub"))
                result.AmountOfRubPaid = tempDictionary["rub"];
            if (tempDictionary.ContainsKey("del"))
                result.AmountOfDelPaid = tempDictionary["del"];
            if (tempDictionary.ContainsKey("usdt"))
                result.AmountOfUsdtPaid = tempDictionary["usdt"];
            if (tempDictionary.ContainsKey("ton"))
                result.AmountOfTonPaid = tempDictionary["ton"];
        }
       
        private static void MapDataFromUsersAmountDictionary(GetStaticticsDto result, Dictionary<string, IEnumerable<DatePoint>> tempDictionary)
        {
            if (tempDictionary.ContainsKey("rub"))
                result.AmountOfUsersWhoPayRub = tempDictionary["rub"];
            if (tempDictionary.ContainsKey("del"))
                result.AmountOfUsersWhoPayDel = tempDictionary["del"];
            if (tempDictionary.ContainsKey("usdt"))
                result.AmountOfUsersWhoPayUsdt = tempDictionary["usdt"];
            if (tempDictionary.ContainsKey("ton"))
                result.AmountOfUsersWhoPayTon = tempDictionary["ton"];
        }
        public async Task<GetStaticticsDto> GetStats(DateDto dates, string group, string offset)
        {
            var ctx = new clientContext();
            var result = new GetStaticticsDto();
            List<User> temporary;
            if (offset == "interval")
                temporary = await ctx.Users
                 .Where(u => u.CreatedAt <= dates.LastTime
                          && u.CreatedAt >= dates.FirstTime)
                 .ToListAsync();
            else
                temporary = await ctx.Users.ToListAsync();

            foreach (var user in temporary)
                user.CreatedAt = DateGrouper.GroupDate(group, user.CreatedAt);
            result.AmountOfCreatedUsers = temporary
                .GroupBy(u => u.CreatedAt).Select(g => new DatePoint(g.Key, g.Count())).OrderBy(dt => dt.Time);

            var payHistories = ctx.PayHistories.Where(ph => ph.PaidAt != null && ph.PaymentMethod == "tariff");
            if (offset == "interval")
                payHistories = payHistories
                    .Where(ph => ph.PaidAt <= dates.LastTime && ph.PaidAt >= dates.FirstTime);

            var temp = payHistories.AsEnumerable()
              .GroupBy(ph => ph.Currency);

            foreach (var currencyGroup in temp)
                foreach (var transaction in currencyGroup)
                    transaction.PaidAt = DateGrouper.GroupDate(group, (DateTime)transaction.PaidAt);

            var tempDictionary = new Dictionary<string, IEnumerable<DatePoint>>();
            var amountOfPaidDictionary = new Dictionary<string, IEnumerable<DatePoint>>();
            foreach (var currencyGroup in temp)
            {
                var datePointWhoPaidList = new List<DatePoint>();
                List<DatePoint> datePointOfSumPaid = new();
                var temporaryGroup = currencyGroup.AsEnumerable().GroupBy(ph => ph.PaidAt);
                foreach (var dateGroup in temporaryGroup)
                {
                    var enumOfDatePoint = dateGroup.AsEnumerable();
                    int datePointAmount = enumOfDatePoint.DistinctBy(ph => ph.UserId).Count();
                    var amount_Of_Paid_For_Currency_In_DatePoint = enumOfDatePoint.Sum(ph => ph.Price);
                    var date_Amount_Of_Users_Who_Paid_Point = new DatePoint((DateTime)dateGroup.Key, datePointAmount);
                    var date_point_for_sum_paid = new DatePoint((DateTime)dateGroup.Key, amount_Of_Paid_For_Currency_In_DatePoint);
                    datePointWhoPaidList.Add(date_Amount_Of_Users_Who_Paid_Point);
                    datePointOfSumPaid.Add(date_point_for_sum_paid);
                }
                amountOfPaidDictionary[currencyGroup.Key] = datePointOfSumPaid.OrderBy(dt => dt.Time);
                tempDictionary[currencyGroup.Key] = datePointWhoPaidList.OrderBy(dt => dt.Time);
            }
            MapDataFromPaidAmountDictionary(result, amountOfPaidDictionary);
            MapDataFromUsersAmountDictionary(result, tempDictionary);
            return result;
        }
    }
}
