using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class Server
    {
        public int Id { get; set; }
        public string? ApiUrl { get; set; }
        public string? CertSha256 { get; set; }
        public string? Name { get; set; }
        public string? CreatedAt { get; set; }
    }
}
