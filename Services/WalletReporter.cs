using Microsoft.AspNetCore.SignalR;
using web_panel_api.Dto;
using web_panel_api.Models;

namespace web_panel_api.Services
{
    public class WalletReporter : IWalletReporter
    {
        public WalletReport GetReport(ILookup<string?, ILookup<string?, float?>> all, ILookup<string?, float?> frozen, string project)
        {
            var result = new WalletReport();
            var common = new WalletWrapper();
            float? temp;
            var rub = "RUB";
           
            var del = "DEL";
          
            var trx = "TRX";
            var bnb = "BNB";
            var ton = "TON";

            temp = all["input"]?.FirstOrDefault()?[rub]?.FirstOrDefault();
            if (temp != null)
                common.RUB = temp;
            temp = all["input"]?.FirstOrDefault()?[del]?.FirstOrDefault();
            if (temp != null)
                common.DEL = temp;
            temp = all["input"]?.FirstOrDefault()?[trx]?.FirstOrDefault();
            if (temp != null)
                common.TRX = temp;
            temp = all["input"]?.FirstOrDefault()?[ton]?.FirstOrDefault();
            if (temp != null)
                common.TON = temp;
            temp = all["input"]?.FirstOrDefault()?[bnb]?.FirstOrDefault();
            if (temp != null)
                common.BNB = temp;

            temp = all["output"]?.FirstOrDefault()?[rub]?.FirstOrDefault();
            if (temp != null)
                common.RUB -= temp;
            temp = all["output"]?.FirstOrDefault()?[del]?.FirstOrDefault();
            if (temp != null)
                common.DEL -= temp;
            temp = all["output"]?.FirstOrDefault()?[ton]?.FirstOrDefault();
            if (temp != null)
                common.TON -= temp;
            temp = all["output"]?.FirstOrDefault()?[bnb]?.FirstOrDefault();
            if (temp != null)
                common.BNB -= temp;
            temp = all["output"]?.FirstOrDefault()?[trx]?.FirstOrDefault();
            if (temp != null)
                common.TRX -= temp;
            result.All = common;
           
            var frozenBalance = new WalletWrapper
            {
                RUB = frozen[rub]?.FirstOrDefault() ?? 0,
                TRX = (frozen[trx]?.FirstOrDefault() ?? 0),
                TON = frozen[ton]?.FirstOrDefault() ?? 0,
                DEL = frozen[del]?.FirstOrDefault() ?? 0,
                BNB = frozen[bnb]?.FirstOrDefault() ?? 0,
            };
            result.Frozen = frozenBalance;

            result.Available = new WalletWrapper
            {
                RUB = common.RUB - frozenBalance.RUB,
                TRX = common.TRX - frozenBalance.TRX,
                BNB = common.BNB - frozenBalance.BNB,
                DEL = common.DEL - frozenBalance.DEL,
                TON = common.TON - frozenBalance.TON
            };

            return result;
        }
    }
}
