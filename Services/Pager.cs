using Microsoft.EntityFrameworkCore;
using web_panel_api.Models;

namespace web_panel_api.Services
{
    public class Pager<T>
    {
        public static async Task<IEnumerable<T>> GetPagedEnumerable(IQueryable<T> query, string sortParam, string sortOrder, int page, int  pageSize)
        {
            var result = new List<T>();
            var keySelector = Filter<T>.CreateKeySelector(sortParam);
            if (sortOrder == "desc")
                query = query.OrderByDescending(keySelector);
            else
                query = query.OrderBy(keySelector);
            result = await query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
            return result;
        }
    }
}
