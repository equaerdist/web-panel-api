using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class PayHistory
    {
        public int Id { get; set; }
        public long? UserId { get; set; }
        public float? Price { get; set; }
        public string? Currency { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentType { get; set; }
        public int? TariffId { get; set; }
        public sbyte? StatusPay { get; set; }
        public string? Description { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Tariff? Tariff { get; set; }
        public virtual User? User { get; set; }
    }
}
