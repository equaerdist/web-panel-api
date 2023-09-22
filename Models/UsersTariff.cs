using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class UsersTariff
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public int? TariffId { get; set; }
        public int Duration { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Tariff? Tariff { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
