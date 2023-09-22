using Microsoft.AspNetCore.SignalR;
using web_panel_api.Dto;
using web_panel_api.Models;

namespace web_panel_api.Services
{
    public class WalletReporter : IWalletReporter
    {
        public WalletReport GetReport(ILookup<string?, ILookup<string?, float?>> all, ILookup<string?, float?> frozen)
        {
            var result = new WalletReport();
            var common = new WalletWrapper();
            float? temp;
            temp = all["input"].FirstOrDefault()?["rub"].FirstOrDefault();
            if (temp != null)
                common.RUB = temp;
            temp = all["input"].FirstOrDefault()?["del"].FirstOrDefault();
            if (temp != null)
                common.DEL = temp;
            temp = all["input"].FirstOrDefault()?["usdt"].FirstOrDefault();
            if (temp != null)
                common.USDT = temp;
            temp = all["input"].FirstOrDefault()?["ton"].FirstOrDefault();
            if (temp != null)
                common.TON = temp;

            temp = all["output"].FirstOrDefault()?["rub"].FirstOrDefault();
            if (temp != null)
                common.RUB -= temp;
            temp = all["output"].FirstOrDefault()?["del"].FirstOrDefault();
            if (temp != null)
                common.DEL -= temp;
            temp = all["output"].FirstOrDefault()?["ton"].FirstOrDefault();
            if (temp != null)
                common.TON -= temp;
            temp = all["output"].FirstOrDefault()?["usdt"].FirstOrDefault();
            if (temp != null)
                common.USDT -= temp;
            result.All = common;
            var frozenBalance = new WalletWrapper
            {
                RUB = frozen["rub"].FirstOrDefault() ?? 0,
                USDT = (frozen["usdt_bep20"].FirstOrDefault() ?? 0) + (frozen["usdt_trx20"].FirstOrDefault() ?? 0),
                TON = frozen["ton"].FirstOrDefault() ?? 0,
                DEL = frozen["del"].FirstOrDefault() ?? 0
            };
            result.Frozen = frozenBalance;

            result.Available = new WalletWrapper
            {
                RUB = common.RUB - frozenBalance.RUB,
                USDT = common.USDT - frozenBalance.USDT,
                DEL = common.DEL - frozenBalance.DEL,
                TON = common.TON - frozenBalance.TON
            };

            return result;
        }
    }
}
