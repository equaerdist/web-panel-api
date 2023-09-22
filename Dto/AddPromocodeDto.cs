using System.ComponentModel.DataAnnotations;

namespace web_panel_api.Dto
{
    public class AddPromocodeDto
    {
        [Required]
        public string TariffName { get; set; } = null!;
    }
}
