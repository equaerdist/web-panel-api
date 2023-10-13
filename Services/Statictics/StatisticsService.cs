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
            if (tempDictionary.ContainsKey("RUB"))
                result.AmountOfRubPaid = tempDictionary["RUB"];
            if (tempDictionary.ContainsKey("DEL"))
                result.AmountOfDelPaid = tempDictionary["DEL"];
            if (tempDictionary.ContainsKey("USDT"))
                result.AmountOfUsdtPaid = tempDictionary["USDT"];
            if (tempDictionary.ContainsKey("TON"))
                result.AmountOfTonPaid = tempDictionary["TON"];
        }
       
        private static void MapDataFromUsersAmountDictionary(GetStaticticsDto result, Dictionary<string, IEnumerable<DatePoint>> tempDictionary)
        {
            if (tempDictionary.ContainsKey("RUB"))
                result.AmountOfUsersWhoPayRub = tempDictionary["RUB"];
            if (tempDictionary.ContainsKey("DEL"))
                result.AmountOfUsersWhoPayDel = tempDictionary["DEL"];
            if (tempDictionary.ContainsKey("USDT"))
                result.AmountOfUsersWhoPayUsdt = tempDictionary["USDT"];
            if (tempDictionary.ContainsKey("TON"))
                result.AmountOfUsersWhoPayTon = tempDictionary["TON"];
        }
        public async Task<GetStaticticsDto> GetStats(DateDto dates, string group, string offset, string project)
        {
            var result = new GetStaticticsDto();
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                List<User> temporary;
                if (offset == "interval")
                    temporary = await ctx.Users
                     .Where(u => u.CreatedAt != null && u.CreatedAt <= dates.LastTime
                              && u.CreatedAt >= dates.FirstTime)
                     .ToListAsync();
                else
                    temporary = await ctx.Users.Where(u => u.CreatedAt != null).ToListAsync();

                foreach (var user in temporary)
                {
                    if (user.CreatedAt is null) throw new ArgumentNullException(nameof(user.CreatedAt));
                    user.CreatedAt = DateGrouper.GroupDate(group, (DateTime)user.CreatedAt);
                }
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
                    {
                        if (transaction.PaidAt is null) throw new ArgumentNullException();
                        transaction.PaidAt = DateGrouper.GroupDate(group, (DateTime)transaction.PaidAt);
                    }

                var tempDictionary = new Dictionary<string, IEnumerable<DatePoint>>();
                var amountOfPaidDictionary = new Dictionary<string, IEnumerable<DatePoint>>();
                foreach (var currencyGroup in temp)
                {
                    var datePointWhoPaidList = new List<DatePoint>();
                    List<DatePoint> datePointOfSumPaid = new();
                    var temporaryGroup = currencyGroup.AsEnumerable().GroupBy(ph => ph.PaidAt);
                    foreach (var dateGroup in temporaryGroup)
                    {
                        if (dateGroup.Key is null) throw new ArgumentNullException();
                        var enumOfDatePoint = dateGroup.AsEnumerable();
                        int datePointAmount = enumOfDatePoint.DistinctBy(ph => ph.UserId).Count();
                        var amount_Of_Paid_For_Currency_In_DatePoint = enumOfDatePoint.Sum(ph => ph.Price);
                        var date_Amount_Of_Users_Who_Paid_Point = new DatePoint((DateTime)dateGroup.Key, datePointAmount);
                        var date_point_for_sum_paid = new DatePoint((DateTime)dateGroup.Key, amount_Of_Paid_For_Currency_In_DatePoint);
                        datePointWhoPaidList.Add(date_Amount_Of_Users_Who_Paid_Point);
                        datePointOfSumPaid.Add(date_point_for_sum_paid);
                    }
                    if (currencyGroup.Key is null) throw new ArgumentNullException();
                    amountOfPaidDictionary[currencyGroup.Key] = datePointOfSumPaid.OrderBy(dt => dt.Time);
                    tempDictionary[currencyGroup.Key] = datePointWhoPaidList.OrderBy(dt => dt.Time);
                }
                MapDataFromPaidAmountDictionary(result, amountOfPaidDictionary);
                MapDataFromUsersAmountDictionary(result, tempDictionary);
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                List<web_panel_api.Models.god_eyes.User> temporary;
                if (offset == "interval")
                    temporary = await ctx.Users
                     .Where(u => u.CreatedAt != null && u.CreatedAt <= dates.LastTime
                              && u.CreatedAt >= dates.FirstTime)
                     .ToListAsync();
                else
                    temporary = await ctx.Users.Where(u => u.CreatedAt != null).ToListAsync();

                foreach (var user in temporary)
                {
                    if (user.CreatedAt is null) throw new ArgumentNullException(nameof(user.CreatedAt));
                    user.CreatedAt = DateGrouper.GroupDate(group, (DateTime)user.CreatedAt);
                }
                result.AmountOfCreatedUsers = temporary
                    .GroupBy(u => u.CreatedAt).Select(g => new DatePoint(g.Key, g.Count())).OrderBy(dt => dt.Time);

                var payHistories = ctx.Pays.Where(ph => ph.PaidAt != null && ph.Method == "TARIFF");
                if (offset == "interval")
                    payHistories = payHistories
                        .Where(ph => ph.PaidAt <= dates.LastTime && ph.PaidAt >= dates.FirstTime);

                var temp = payHistories.AsEnumerable()
                  .GroupBy(ph => ph.Currency);

                foreach (var currencyGroup in temp)
                    foreach (var transaction in currencyGroup)
                    {
                        if (transaction.PaidAt is null) throw new ArgumentNullException();
                        transaction.PaidAt = DateGrouper.GroupDate(group, (DateTime)transaction.PaidAt);
                    }

                var tempDictionary = new Dictionary<string, IEnumerable<DatePoint>>();
                var amountOfPaidDictionary = new Dictionary<string, IEnumerable<DatePoint>>();
                foreach (var currencyGroup in temp)
                {
                    var datePointWhoPaidList = new List<DatePoint>();
                    List<DatePoint> datePointOfSumPaid = new();
                    var temporaryGroup = currencyGroup.AsEnumerable().GroupBy(ph => ph.PaidAt);
                    foreach (var dateGroup in temporaryGroup)
                    {
                        if (dateGroup.Key is null) throw new ArgumentNullException();
                        var enumOfDatePoint = dateGroup.AsEnumerable();
                        int datePointAmount = enumOfDatePoint.DistinctBy(ph => ph.UserId).Count();
                        var amount_Of_Paid_For_Currency_In_DatePoint = enumOfDatePoint.Sum(ph => ph.Price);
                        var date_Amount_Of_Users_Who_Paid_Point = new DatePoint((DateTime)dateGroup.Key, datePointAmount);
                        var date_point_for_sum_paid = new DatePoint((DateTime)dateGroup.Key, amount_Of_Paid_For_Currency_In_DatePoint);
                        datePointWhoPaidList.Add(date_Amount_Of_Users_Who_Paid_Point);
                        datePointOfSumPaid.Add(date_point_for_sum_paid);
                    }
                    if (currencyGroup.Key is null) throw new ArgumentNullException();
                    amountOfPaidDictionary[currencyGroup.Key] = datePointOfSumPaid.OrderBy(dt => dt.Time);
                    tempDictionary[currencyGroup.Key] = datePointWhoPaidList.OrderBy(dt => dt.Time);
                }
                MapDataFromPaidAmountDictionary(result, amountOfPaidDictionary);
                MapDataFromUsersAmountDictionary(result, tempDictionary);
            }
            return result;
        }
    }
}
