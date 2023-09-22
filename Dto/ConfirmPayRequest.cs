using System.ComponentModel.DataAnnotations;

namespace web_panel_api.Dto
{
    public class ConfirmPayRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public sbyte StatusPay { get; set; }
    }
}
