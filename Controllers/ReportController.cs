using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Drawing.Chart.ChartEx;
using web_panel_api.Dto;
using web_panel_api.Models;
using web_panel_api.Services;

namespace web_panel_api.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("/api/report")]
    public class ReportController : Controller
    {
        private readonly IWalletReporter _rpt;

        public ReportController(IWalletReporter rpt) { _rpt = rpt; }

        [HttpPost("wallet")]
        public async Task<WalletReport> GetWalletStat(DateDto info, string offset, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var context = new clientContext();
                var all = context.PayHistories
                    .Where(e => e.PaymentMethod != "referrals" && e.PaidAt != null);
                if (offset == "interval")
                    all = all.Where(e => e.PaidAt >= info.FirstTime && e.PaidAt <= info.LastTime);
                var temp = all.AsEnumerable()
                                .GroupBy(e => e.PaymentType)
                                .ToLookup(g => g.Key, g => g.GroupBy(ph => ph.Currency).ToLookup(phc => phc.Key, phc => phc.Sum(el => el.Price)));
                var frozen = context.Wallets.AsEnumerable().GroupBy(w => w.Type)
                   .ToLookup(e => e.Key, e => e.Sum(we => we.Balance));
                var result = Task.Run(() => _rpt.GetReport(temp, frozen, project));
                return await result;
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var all = ctx.Pays.Where(e => e.Method != "REFERRALS" && e.PaidAt != null);
                if (offset == "interval")
                    all = all.Where(e => e.PaidAt >= info.FirstTime && e.PaidAt <= info.LastTime);
                var temp = all.AsEnumerable()
                    .GroupBy(e => e.Type)
                    .ToLookup(g => g.Key, g => g.GroupBy(ph => ph.Currency).ToLookup(phc => phc.Key, phc => phc.Sum(el => el.Price)));
                var frozen = ctx.Wallets.AsEnumerable().GroupBy(w => w.Type)
                    .ToLookup(g => g.Key, g => g.Sum(we => we.Balance));
                var result = Task.Run(() => _rpt.GetReport(temp, frozen, project));
                return await result;
            }
        }
        [HttpGet("referral")]
        public async Task<ReferralReport> GetReferralAward(string project)
        {
            var result = new ReferralReport();
            float allRub = 0, allUsdt = 0, allTon = 0, allDel = 0;
            float givenRub = 0, givenUsdt = 0, givenTon = 0, givenDel = 0;
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                foreach (var ph in await ctx.PayHistories.ToListAsync())
                {
                    switch (ph.Currency)
                    {
                        case "RUB":
                            allRub += ph.Price ?? 0;
                            break;
                        case "USDT":
                            allUsdt += ph.Price ?? 0;
                            break;
                        case "DEL":
                            allDel += ph.Price ?? 0;
                            break;
                        case "TON":
                            allTon += ph.Price ?? 0;
                            break;
                    }
                    if (ph.PaymentMethod == "referrals")
                    {
                        switch (ph.Currency)
                        {
                            case "RUB":
                                givenRub += ph.Price ?? 0;
                                break;
                            case "USDT":
                                givenUsdt += ph.Price ?? 0;
                                break;
                            case "DEL":
                                givenDel += ph.Price ?? 0;
                                break;
                            case "TON":
                                givenTon += ph.Price ?? 0;
                                break;
                        }
                    }
                }
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                foreach (var ph in await ctx.Pays.ToListAsync())
                {
                    switch (ph.Currency)
                    {
                        case "RUB":
                            allRub += ph.Price ?? 0;
                            break;
                        case "USDT":
                            allUsdt += ph.Price ?? 0;
                            break;
                        case "DEL":
                            allDel += ph.Price ?? 0;
                            break;
                        case "TON":
                            allTon += ph.Price ?? 0;
                            break;
                    }
                    if (ph.Method == "REFERRALS")
                    {
                        switch (ph.Currency)
                        {
                            case "RUB":
                                givenRub += ph.Price ?? 0;
                                break;
                            case "USDT":
                                givenUsdt += ph.Price ?? 0;
                                break;
                            case "DEL":
                                givenDel += ph.Price ?? 0;
                                break;
                            case "TON":
                                givenTon += ph.Price ?? 0;
                                break;
                        }
                    }
                }
            }
            result.All = new WalletWrapper { DEL = allDel, RUB = allRub, USDT = allUsdt, TON = allTon };
            result.Given = new WalletWrapper { DEL = givenDel, RUB = givenRub, USDT = givenUsdt, TON = givenTon };
            result.Saved = new WalletWrapper {  DEL = allDel - givenDel, RUB = allRub - givenRub, TON = allTon - givenTon, USDT = allUsdt - givenUsdt };
            return result;
        }
        [HttpGet("user")]
        public async Task<UserReport> GetUserReport(string project)
        {
            var result = new UserReport();
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                foreach (var user in await ctx.Users.ToListAsync())
                {
                    result.All++;
                    if (user.IsReplay == 1)
                        result.RepeatedPay++;
                    if (user.StatusTariff == 1)
                        result.Active++;
                    else
                        result.NotActive++;
                }
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                foreach(var user in await ctx.Users.ToListAsync())
                {
                    result.All++;
                    if (user.IsReplay == true)
                        result.RepeatedPay++;
                    if (user.Status == true)
                        result.Active++;
                    else
                        result.NotActive++;
                }
            }
            return result;
        }
    }
}
