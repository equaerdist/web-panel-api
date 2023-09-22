namespace web_panel_api.Dto
{
    public class WalletReport
    {
        public WalletWrapper All { get; set; } = null!;
        public WalletWrapper Frozen { get; set; } = null!;
        public WalletWrapper Available { get; set; } = null!;
    }
}
