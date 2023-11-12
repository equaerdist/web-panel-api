using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class PromocodesPrice
    {
        public int Id { get; set; }
        public int? PromocodeId { get; set; }
        public float? Price { get; set; }
        public string? Currency { get; set; }

        public virtual Promocode? Promocode { get; set; }
    }
}
