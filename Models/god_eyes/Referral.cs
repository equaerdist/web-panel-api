using System;
using System.Collections.Generic;

namespace web_panel_api.Models.god_eyes
{
    public partial class Referral
    {
        public long ChildId { get; set; }
        public long? ParentId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User Child { get; set; } = null!;
        public virtual User? Parent { get; set; }
    }
}
