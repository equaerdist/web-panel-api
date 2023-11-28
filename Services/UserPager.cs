using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using web_panel_api.Models;

namespace web_panel_api.Services
{
    public class UserPager
    {
        public async static Task<IEnumerable<User>> GetPagedUserForVpnService(IQueryable<User> query, string sortParam, string sortOrder, int page, int pageSize)
        {
            var group = sortParam.Split('.');
            Expression<Func<User, object>> keySelectorSecond = sortParam switch
            {
                _ => user => user.UsersKeys
                .FirstOrDefault(uk => uk.Status == 1 && uk.DateEnd != null) != null
                ? user.UsersKeys.First(uk => uk.Status == 1 && uk.DateEnd != null).DateEnd ?? DateOnly.MinValue
                : DateOnly.MinValue
            };
            if(group != null && group.Length > 1 && group[0].Equals("wallet"))
            {
                keySelectorSecond = user => user.Wallets.FirstOrDefault(w => w.Type != null && w.Type.Equals(group[1])) != null
                ? user.Wallets.First(w => w.Type != null && w.Type.Equals(group[1])).Balance ?? 0 : 0;
            }
            if (sortOrder == "desc")
                query = query.OrderByDescending(keySelectorSecond);
            else
                query = query.OrderBy(keySelectorSecond);
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async static Task<IEnumerable<web_panel_api.Models.god_eyes.User>> GetPagedUserForGodEyes(IQueryable<web_panel_api.Models.god_eyes.User> query, string sortParam, string sortOrder, int page, int pageSize)
        {
            var group = sortParam.Split('.');
            Expression<Func<web_panel_api.Models.god_eyes.User, object>> keySelectorSecond = sortParam switch
            {
                _ => user => user.Id
            } ;
            if(group != null && group.Length > 1 && group[0].Equals("wallet"))
            {
                keySelectorSecond = user => user.Wallets.FirstOrDefault(w => w.Currency == group[1]) != null
                ? user.Wallets.First(w => w.Currency == (group[1])).Balance  ?? 0 : 0;
            }
            if (sortOrder == "desc")
                query = query.OrderByDescending(keySelectorSecond);
            else
                query = query.OrderBy(keySelectorSecond);
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
