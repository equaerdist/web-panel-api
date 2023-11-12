using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class Wallet
    {
        public long UserId { get; set; }
        public string? Type { get; set; }
        public float? Balance { get; set; }
        public float? BalanceFact { get; set; }
        public string? Currency { get; set; }
        public string? Addresse { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
