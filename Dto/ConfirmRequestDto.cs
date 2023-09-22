using System.ComponentModel.DataAnnotations;

namespace web_panel_api.Dto
{
    public class ConfirmRequestDto
    {
        [Required]
        public List<ConfirmPayRequest> PayRequests { get; set; } = null!;
    }
}
