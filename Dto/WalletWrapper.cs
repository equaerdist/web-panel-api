namespace web_panel_api.Dto
{
    //BNB, TRX, TON,DEL,RUB
    public class WalletWrapper
    {
        public float? RUB { get; set; }
        public float? TRX { get; set; }
        public float? TON { get; set; }
        public float? DEL { get; set; }
        public float? BNB { get; set; }
        public float? USDT { get; set; }
        public WalletWrapper()
        {
            RUB = 0; BNB = 0; TON = 0; DEL = 0;
            TRX = 0;
            USDT = 0;
        }
    }
}
