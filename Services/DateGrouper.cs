using web_panel_api.Models;

namespace web_panel_api.Services
{
    public class DateGrouper
    {
        private static DateTime GroupByDay(DateTime dateTime)
        {
            return new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day);
        }
        private static DateTime GroupByMonth(DateTime dateTime) {
            return new DateTime(
            dateTime.Year,
            dateTime.Month, 1);
        }
        private static DateTime GroupByYear(DateTime dateTime)
        {
            return new DateTime(
            dateTime.Year, 1, 1);
        }
        public static  DateTime GroupDate(string group, DateTime manip)
        {
            var result = new DateTime();
            switch (group)
            {
                case "day":
                    result = DateGrouper.GroupByDay(manip); break;
                case "month":
                    result = DateGrouper.GroupByMonth(manip); break;
                case "year":
                    result = DateGrouper.GroupByYear(manip); break;
                default:
                    throw new ArgumentException();
            }
            return result;
        }
    }
}
