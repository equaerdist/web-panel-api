using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class Setting
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
        public int? MinOutputRub { get; set; }
        public int? MinOutputDel { get; set; }
        public int? MinOutputTon { get; set; }
        public int? MinOutputUsdt { get; set; }
    }
}
