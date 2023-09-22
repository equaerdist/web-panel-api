using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class PayHistory
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public float? Price { get; set; }
        public string Currency { get; set; } = null!;
        public string? PaymentMethod { get; set; }
        public string? PaymentType { get; set; }
        public sbyte? StatusPay { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
