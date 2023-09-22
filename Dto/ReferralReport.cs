namespace web_panel_api.Dto
{
    public class ReferralReport
    {
        public WalletWrapper All { get; set; } = null!;
        public WalletWrapper Given { get; set; } = null!;
        public WalletWrapper Saved { get; set; } = null!;
    }
}
