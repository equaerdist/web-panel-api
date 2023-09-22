using System.ComponentModel.DataAnnotations;

namespace web_panel_api.Dto
{
    public class AddTariffDto
    {
        [Required]
        public string TariffName { get; set; } = null!;
        [Required]
        public int Duration { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
