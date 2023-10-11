using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class SendMessage
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? Type { get; set; }
        public sbyte? Send { get; set; }
        public DateTime? DateCreate { get; set; }
    }
}
