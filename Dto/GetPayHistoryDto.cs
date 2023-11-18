using web_panel_api.Models;

namespace web_panel_api.Dto
{
    public class GetPayHistoryDto
    {
        public int Id { get; set; }
        public float? Price { get; set; }
        public string? Currency { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Method { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? Status { get; set; }
        public sbyte? StatusPay { get; set; }
        public string? Description { get; set; }

        public GetUserDto User { get; set; } = null!;
    }
}
