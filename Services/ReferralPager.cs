using System.Linq.Expressions;
using web_panel_api.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace web_panel_api.Services
{
    public class ReferralPager
    {
        public static Expression<Func<User, object>> GetSelector(string sortParam)
        {
            Expression<Func<User, object>> result;
            int status;
            switch (sortParam)
            {
                case "active":
                    status = 1;
                    break;
                default:
                    status = 0;
                    break;
            }
            result = user => user.ReferralsTreeParents.Count(t => t.Child.Status == status);
            return result;
        }
     }
}
