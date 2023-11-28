using web_panel_api.Models;
using web_panel_api.Models.god_eyes;

namespace web_panel_api.Dto
{
    public class GetUserDto
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public sbyte? IsReplay { get; set; }
        public sbyte? IsFree { get; set; }
        public sbyte? StatusTariff { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<UsersKey> UsersKeys { get; set; } = null!;
        public List<Models.Wallet> Wallets { get; set; } = null!;
    }
}
