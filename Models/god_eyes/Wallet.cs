using System;
using System.Collections.Generic;

namespace web_panel_api.Models.god_eyes
{
    public partial class Wallet
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string? Currency { get; set; }
        public float? Balance { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
