using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class Tariff
    {
        public Tariff()
        {
            PayHistories = new HashSet<PayHistory>();
            Promocodes = new HashSet<Promocode>();
            UsersKeys = new HashSet<UsersKey>();
            UsersTariffs = new HashSet<UsersTariff>();
        }

        public int Id { get; set; }
        public string? TariffName { get; set; }
        public int? Duration { get; set; }
        public int? Price { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<PayHistory> PayHistories { get; set; }
        public virtual ICollection<Promocode> Promocodes { get; set; }
        public virtual ICollection<UsersKey> UsersKeys { get; set; }
        public virtual ICollection<UsersTariff> UsersTariffs { get; set; }
    }
}
