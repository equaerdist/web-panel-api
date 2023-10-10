using System;
using System.Collections.Generic;

namespace web_panel_api.Models.god_eyes
{
    public partial class Setting
    {
        public long Id { get; set; }
        public long? RequestCost { get; set; }
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
        public float? MinOutput { get; set; }
        public float? MaxOutput { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
