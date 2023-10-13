using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using web_panel_api.Services.JsonConverters;

namespace web_panel_api.Models
{
    public partial class UsersKey
    {
        public int Id { get; set; }
        public long? UserId { get; set; }
        public long? ServerId { get; set; }
        public int? KeyId { get; set; }
        public string? KeyUrl { get; set; }
        public int? TariffId { get; set; }
        public sbyte? Status { get; set; }
     
        public DateOnly? DateEnd { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Tariff? Tariff { get; set; }
        public virtual User? User { get; set; }
    }
}
