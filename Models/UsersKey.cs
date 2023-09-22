using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class UsersKey
    {
        public int Id { get; set; }
        public long? UserId { get; set; }
        public int? ServerId { get; set; }
        public string? Key { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual Server? Server { get; set; }
        public virtual User? User { get; set; }
    }
}
