namespace web_panel_api.Dto
{
    public class SettingDto
    {
        public int Id { get; set; }
        public float? CommissionOutputDel { get; set; }
        public float? CommissionOutputTon { get; set; }
        public float? CommissionOutputUsdt { get; set; }
        public float? CommissionOutputRub { get; set; }
        public float? CommissionInputDel { get; set; }
        public float? CommissionInputTon { get; set; }
        public float? CommissionInputUsdt { get; set; }
        public float? CommissionInputRub { get; set; }
        public float? RefferalRewardLvl1 { get; set; }
        public float? RefferalRewardLvl2 { get; set; }
        public int? ReferralRewardLvl1 { get; set; }
        public int? ReferralRewardLvl2 { get; set; }
        public int? ReferralRewardLvl3 { get; set; }
    }
}
