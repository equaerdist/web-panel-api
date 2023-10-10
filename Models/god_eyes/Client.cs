using System;
using System.Collections.Generic;

namespace web_panel_api.Models.god_eyes
{
    public partial class Client
    {
        public string Name { get; set; } = null!;
        public long? ApiId { get; set; }
        public string? ApiHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SessionString { get; set; }
        public long? IsUsedBy { get; set; }
        public long? RequestsBalance { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? IsUsedByNavigation { get; set; }
    }
}
