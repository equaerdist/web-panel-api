using System;
using System.Collections.Generic;

namespace web_panel_api.Models
{
    public partial class Server
    {
        public Server()
        {
            UsersKeys = new HashSet<UsersKey>();
        }

        public int Id { get; set; }
        public string? Apiurl { get; set; }
        public string? Certsha256 { get; set; }
        public string? CreatedAt { get; set; }

        public virtual ICollection<UsersKey> UsersKeys { get; set; }
    }
}
