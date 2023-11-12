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
            int status = 1;
            int statusFlag = 1;
            switch (sortParam)
            {
                case "active":
                    status = 1;
                    break;
                case "dateEnd":
                    statusFlag = 0;
                    break;
                default:
                    status = 0;
                    break;
            }
            if (statusFlag == 1)
                result = user => user.ReferralsTreeParents.Count(t => t.Children.Status == status);
            else
            {
                result = user => user.UsersKeys.FirstOrDefault(uk => uk.Status == 1) != null ? user.UsersKeys.First(uk => uk.Status == 1).DateEnd : DateOnly.MinValue;
            }


            return result;
        }
        public static Expression<Func<web_panel_api.Models.god_eyes.User, object>> GetSecondSelector(string sortParam)
        {
            Expression<Func<web_panel_api.Models.god_eyes.User, object>> result;
            bool status;
            switch (sortParam)
            {
                case "active":
                    status = true;
                    break;
                default:
                    status = false;
                    break;
            }
            result = user => user.ReferralParents.Count(t => t.Child.Status == status);
            return result;
        }
    }
}
