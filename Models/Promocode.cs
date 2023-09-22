using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class Promocode
    {
        public Promocode()
        {
            UsePromocodes = new HashSet<UsePromocode>();
        }

        public int Id { get; set; }
        public string ValueCode { get; set; } = null!;
        public long? UserId { get; set; }
        public int TariffId { get; set; }
        public float? PriceDel { get; set; }
        public float? PriceTon { get; set; }
        public float? PriceUsdt { get; set; }
        public float? PriceRub { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Tariff Tariff { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<UsePromocode> UsePromocodes { get; set; }
    }
}
