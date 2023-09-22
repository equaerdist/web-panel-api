namespace web_panel_api.Dto
{
    public class UserReport
    {
        public int All { get; set; } = 0;
        public int Active { get;set; } = 0;
        public int NotActive { get; set; } = 0;
        public int RepeatedPay { get;set; } = 0;
    }
}
