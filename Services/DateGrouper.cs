using web_panel_api.Models;

namespace web_panel_api.Services
{
    public class DateGrouper
    {
        public static DateTime GroupByDay(DateTime dateTime)
        {
            return new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day);
        }
        public static DateTime GroupByMonth(DateTime dateTime) {
            return new DateTime(
            dateTime.Year,
            dateTime.Month, 1);
        }
        public static DateTime GroupByYear(DateTime dateTime)
        {
            return new DateTime(
            dateTime.Year, 1, 1);
        }
    }
}
