using System.ComponentModel.DataAnnotations;

namespace web_panel_api.Dto
{
    public class ResolveFreeSubDto
    {
        [Required]
        public long Id { get;set; }
        [Required]
        public int Duration { get; set; }
    }
}
