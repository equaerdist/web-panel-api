using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class ReferralsTree
    {
        public long ParentId { get; set; }
        public long ChildrenId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long Id { get; set; }

        public virtual User Children { get; set; } = null!;
        public virtual User Parent { get; set; } = null!;
    }
}
