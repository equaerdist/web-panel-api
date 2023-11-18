using System;
using System.Collections.Generic;

namespace web_panel_api.Models.god_eyes
{
    public partial class Pay
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public float? Price { get; set; }
        public string? Currency { get; set; }
        public string? Method { get; set; }
        public int? Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Description { get; set; }

        public virtual User? User { get; set; }
    }
}
