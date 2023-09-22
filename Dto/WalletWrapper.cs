namespace web_panel_api.Dto
{
    public class WalletWrapper
    {
        public float? RUB { get; set; }
        public float? USDT { get; set; }
        public float? TON { get; set; }
        public float? DEL { get; set; }
        public WalletWrapper()
        {
            RUB = 0; USDT = 0; TON = 0; DEL = 0;
        }
    }
}
