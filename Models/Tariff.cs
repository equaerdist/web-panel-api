using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class Tariff
    {
        public Tariff()
        {
            Promocodes = new HashSet<Promocode>();
            UsersTariffs = new HashSet<UsersTariff>();
        }

        public int Id { get; set; }
        public string TariffName { get; set; } = null!;
        public int? Duration { get; set; }
        public int? Price { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Promocode> Promocodes { get; set; }
        public virtual ICollection<UsersTariff> UsersTariffs { get; set; }
    }
}
