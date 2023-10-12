using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using web_panel_api.Models;

namespace web_panel_api.Services
{
    public class UserPager
    {
        public async static Task<IEnumerable<User>> GetPagedUserForVpnService(IQueryable<User> query, string sortParam, string sortOrder, int page, int pageSize)
        {
            Expression<Func<User, object>> keySelectorSecond = sortParam switch
            {
                _ => user => user.UsersKeys
                .FirstOrDefault(uk => uk.Status == 1) == null ? DateTime.MinValue : user.UsersKeys.First(uk => uk.Status == 1).DateEnd
            };
            if (sortOrder == "desc")
                query = query.OrderByDescending(keySelectorSecond);
            else
                query = query.OrderBy(keySelectorSecond);
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
