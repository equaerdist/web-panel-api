using System;
using System.Collections.Generic;

namespace web_panel_api.Models.god_eyes
{
    public partial class Balance
    {
        public long UserId { get; set; }
        public float? Value { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
