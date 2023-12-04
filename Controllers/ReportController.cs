using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Drawing.Chart.ChartEx;
using System.Diagnostics.Eventing.Reader;
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
        const string bnb = "BNB";
        const string trx = "TRX";
        const string ton = "TON";
        const string del = "DEL";
        const string rub = "RUB";
        const string usdt = "USDT";

        public ReportController(IWalletReporter rpt) { _rpt = rpt; }

        [HttpPost("wallet")]
        public async Task<WalletReport> GetWalletStat(DateDto info, string offset, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var context = new clientContext();
                var all = context.PayHistories
                    .AsNoTracking()
                    .Where(e => e.PaymentMethod != "referral" && e.CreateAt != null && e.StatusPay == 1);
                if (offset == "interval")
                    all = all.Where(e => e.CreateAt >= info.FirstTime && e.CreateAt <= info.LastTime);
                var temp = all.AsEnumerable()
                                .GroupBy(e => e.PaymentType)
                                .ToLookup(g => g.Key, g => g.GroupBy(ph => ph.Currency)
                                .ToLookup(phc => phc.Key, phc => phc.Sum(el => el.Price)));
                var frozen = context.Wallets.AsEnumerable().GroupBy(w => w.Type)
                   .ToLookup(e => e.Key, e => e.Sum(we => we.Balance));
                var result = Task.Run(() => _rpt.GetReport(temp, frozen, project));
                return await result;
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var query = ctx.Pays.Where(ph => ph.Status == 1 && ph.CreatedAt != null);
                if(offset.Equals("interval"))
                    query = query.Where(ph => ph.CreatedAt >= info.FirstTime && ph.CreatedAt <= info.LastTime);
                var all = new WalletWrapper();
                float frozenUsdt = 0;
                foreach (var ph in query)
                {
                    if (ph.Method == "BALANCE")
                    {
                        switch (ph.Currency)
                        {
                            case trx:
                                all.TRX += ph.Price ?? 0;
                                break;
                            case usdt:
                                all.USDT += ph.Price ?? 0;
                                break;
                            case ton:
                                all.TON += ph.Price ?? 0;
                                break;
                            case del:
                                all.DEL += ph.Price ?? 0;
                                break;
                            case bnb:
                                all.BNB += ph.Price ?? 0;
                                break;
                        }
                    }
                    if(ph.Currency == usdt)
                    {
                        if (ph.Method == "MESSAGE")
                            frozenUsdt -= ph.Price ?? 0;
                        else if(ph.Method == "REFERRALS")
                            frozenUsdt += ph.Price ?? 0;
                    }
                }
                frozenUsdt += all.USDT ?? 0;
                float availableUsdt = (all.USDT ?? 0) - frozenUsdt;
                return new WalletReport()
                {
                    All = all,
                    Frozen = new WalletWrapper() { USDT = frozenUsdt },
                    Available = new WalletWrapper() { USDT = availableUsdt }
                };
            }
        }
        [HttpGet("referral")]
        public async Task<ReferralReport> GetReferralAward(string project)
        {
            var result = new ReferralReport();
            float allRub = 0, allTrx = 0, allTon = 0, allDel = 0, allBnb = 0, allUsdt = 0;
            float givenRub = 0, givenBnb = 0, givenTon = 0, givenDel = 0, givenTrx = 0;
            float savedRub =0, savedBnb =0, savedTon =0, savedDel =0, savedTrx = 0;
            //BNB, TRX, TON,DEL,RUB
         
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                foreach (var ph in await ctx.PayHistories.AsNoTracking().Where(ph => ph.StatusPay == 1 && ph.PaymentMethod == "referral").ToListAsync())
                {
                    switch (ph.Currency)
                    {
                        case rub:
                            allRub += ph.Price ?? 0;
                            break;
                        case bnb:
                            allBnb += ph.Price ?? 0;
                            break;
                        case trx:
                            allTrx += ph.Price ?? 0;
                            break;
                        case ton:
                            allTon += ph.Price ?? 0;
                            break;
                        case del:
                            allDel += ph.Price ?? 0;
                            break;
                    }
                    
                    if (ph.PaymentType == "output")
                    {
                        switch (ph.Currency)
                        {
                            case rub:
                                givenRub += ph.Price ?? 0;
                                break;
                            case bnb:
                                givenBnb += ph.Price ?? 0;
                                break;
                            case del:
                                givenDel += ph.Price ?? 0;
                                break;
                            case ton:
                                givenTon += ph.Price ?? 0;
                                break;
                            case trx:
                                givenTrx += ph.Price ?? 0;
                                break;
                        }
                    }
                    else if(ph.PaymentType == "input")
                    {
                        switch (ph.Currency)
                        {
                            case rub:
                                savedRub += ph.Price ?? 0;
                                break;
                            case bnb:
                                savedBnb += ph.Price ?? 0;
                                break;
                            case del:
                                savedDel += ph.Price ?? 0;
                                break;
                            case ton:
                                savedTon += ph.Price ?? 0;
                                break;
                            case trx:
                                savedTrx += ph.Price ?? 0;
                                break;
                        }
                    }
                }
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                allUsdt = ctx.Pays.AsNoTracking()
                    .Where(ph => ph.Status == 1 && ph.Currency == usdt && ph.Method == "REFERRALS" && ph.Price != null)
                    .Sum(ph => ph.Price) ?? 0;
               
            }
            result.All = new WalletWrapper { DEL = allDel, RUB = allRub, TRX = allTrx, TON = allTon, BNB = allBnb, USDT = allUsdt };
            result.Given = new WalletWrapper { DEL = givenDel, RUB = givenRub, TRX = givenTrx, TON = givenTon, BNB = givenBnb, USDT = 0 };
            result.Saved = new WalletWrapper 
            {  
                DEL = savedDel, 
                RUB = savedRub, 
                TON = savedTon, 
                TRX = savedTrx, 
                BNB = savedBnb,
                USDT = 0
            };
            return result;
        }
        [HttpGet("user")]
        public async Task<UserReport> GetUserReport(string project)
        {
            var result = new UserReport();
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                foreach (var user in await ctx.Users.AsNoTracking().ToListAsync())
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
                foreach(var user in await ctx.Users.AsNoTracking().ToListAsync())
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
