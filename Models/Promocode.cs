using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class Promocode
    {
        public Promocode()
        {
            PromocodesPrices = new HashSet<PromocodesPrice>();
        }

        public int Id { get; set; }
        public string? ValueCode { get; set; }
        public long? UserId { get; set; }
        public int? TariffId { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Tariff? Tariff { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<PromocodesPrice> PromocodesPrices { get; set; }
    }
}
