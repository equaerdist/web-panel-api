using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace web_panel_api.Services
{
    public class AppOptions
    {
        public string ConnectionString { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string AdminLogin { get; set; } = null!;
        public string AdminPassword { get; set; } = null!;
        public SymmetricSecurityKey SymmetricKey 
        {
            get => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        }
    }
}
