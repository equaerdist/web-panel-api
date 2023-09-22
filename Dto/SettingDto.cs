namespace web_panel_api.Dto
{
    public class SettingDto
    {
        public int Id { get; set; }
        public int? CommissionOutputDel { get; set; }
        public int? CommissionOutputTon { get; set; }
        public int? CommissionOutputUsdt { get; set; }
        public int? CommissionOutputRub { get; set; }
        public int? CommissionInputDel { get; set; }
        public int? CommissionInputTon { get; set; }
        public int? CommissionInputUsdt { get; set; }
        public int? ReferralRewardLvl1 { get; set; }
        public int? ReferralRewardLvl2 { get; set; }
        public int? ReferralRewardLvl3 { get; set; }
    }
}
