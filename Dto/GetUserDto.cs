namespace web_panel_api.Dto
{
    public class GetUserDto
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public sbyte? IsReplay { get; set; }
        public sbyte? IsFree { get; set; }
        public sbyte? StatusTariff { get; set; }
        public sbyte? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
