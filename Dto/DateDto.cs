using System.ComponentModel.DataAnnotations;

namespace web_panel_api.Dto
{
    public class DateDto
    {
        [Required]
        public DateTime FirstTime { get; set; }
        [Required]
        public DateTime LastTime { get; set;}
    }
}
