using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class UsePromocode
    {
        public int Id { get; set; }
        public int? PromocodeId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
