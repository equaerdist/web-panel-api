using System;
using System.Collections.Generic;

namespace web_panel_api.Models.god_eyes
{
    public partial class Setting
    {
        public string Name { get; set; } = null!;
        public string? NameUser { get; set; }
        public string? Value { get; set; }
    }
}
