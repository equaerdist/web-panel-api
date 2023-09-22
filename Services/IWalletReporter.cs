using web_panel_api.Dto;
using web_panel_api.Models;

namespace web_panel_api.Services
{
    public interface IWalletReporter
    {
        WalletReport GetReport(ILookup<string?, ILookup<string?, float?>> all, ILookup<string?, float?> frozen);
    }
}
