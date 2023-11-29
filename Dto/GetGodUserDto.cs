using web_panel_api.Models.god_eyes;

namespace web_panel_api.Dto
{
    public class GetGodUserDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsReplay { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Balance Balance { get; set; } = null!;
    }
}
