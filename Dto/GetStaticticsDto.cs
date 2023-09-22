using web_panel_api.Models;

namespace web_panel_api.Dto { 
    public record class DatePoint(DateTime Time, int Amount);
    public  class CurrencyReport
    {
        public string Currency { get; set; } = string.Empty;
        public IEnumerable<DatePoint> DatePoints { get; set; } = new List<DatePoint>();
    }
    public class GetStaticticsDto
    {
        public IEnumerable<DatePoint> AmountOfCreatedUsers { get; set; } = null!;
        public IEnumerable<DatePoint> AmountOfUsersWhoPayUsdt { get; set; } = new List<DatePoint>();
        public IEnumerable<DatePoint> AmountOfUsersWhoPayDel { get; set; } = new List<DatePoint>();
        public IEnumerable<DatePoint> AmountOfUsersWhoPayTon { get; set; } = new List<DatePoint>();
        public IEnumerable<DatePoint> AmountOfUsersWhoPayRub { get; set; } = new List<DatePoint>();
    }
}
