using System.ComponentModel.DataAnnotations;

namespace web_panel_api.Dto
{
    public class AuthDto
    {
        [Required]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
