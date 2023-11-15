using web_panel_api.Models;

namespace web_panel_api.Dto
{
    public class PayHistoryForStat
    {
     
            public float? Price { get; set; }
            public string? Currency { get; set; }
            public string? PaymentMethod { get; set; }
            public string? PaymentType { get; set; }
            public sbyte? StatusPay { get; set; }
            public DateTime? CreateAt { get; set; }
            public long? UserId { get; set; }

    }
}
