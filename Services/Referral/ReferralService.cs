using AutoMapper;
using Microsoft.EntityFrameworkCore;
using web_panel_api.Dto;
using web_panel_api.Models;

namespace web_panel_api.Services.Referral
{
    public class ReferralService : IReferralService
    {
        private readonly IMapper _mapper;

        private List<GetReferralDto> MapRefToDto(IEnumerable<User> temp)
        {
            List<GetReferralDto> result = new();
            foreach (var user in temp)
            {
                int activeAmount = 0;
                int notActive = 0;
                foreach (var tree in user.ReferralsTreeParents)
                {
                    if (tree.Child.Status == 1)
                        activeAmount++;
                    else if (tree.Child.Status == 0)
                        notActive++;
                }
                var temporary = _mapper.Map<GetReferralDto>(user);
                temporary.Active = activeAmount;
                temporary.NotActive = notActive;
                result.Add(temporary);
            }
            return result;
        }
        public ReferralService(IMapper mapper) { _mapper = mapper; }    
        public async Task<IEnumerable<GetReferralDto>> GetReferralsForUser(string? searchTerm, int page, int pageSize, string sortParam, string sortOrder, string? childNode)
        {

            var ctx = new clientContext();
            var query = ctx.Users.AsQueryable();

            if (!string.IsNullOrEmpty(childNode))
            {
                var parent = await ctx.Users
                    .Include(u => u.ReferralsTreeChildren)
                    .Where(u => u.Username.Equals(searchTerm) || u.FirstName.Equals(searchTerm))
                    .SelectMany(u => u.ReferralsTreeChildren)
                    .Include(tr => tr.Parent)
                    .Select(tr => tr.Parent)
                    .FirstOrDefaultAsync();
                if (parent is not null)
                    searchTerm = parent.Username;
                else
                    searchTerm = null;
            }
            if (string.IsNullOrEmpty(searchTerm))

                query = ctx.Users
                    .Include(u => u.ReferralsTreeChildren)
                    .Where(u => u.ReferralsTreeChildren.Count == 0)
                    .Include(u => u.ReferralsTreeParents)
                    .ThenInclude(t => t.Child);
            else
                query = ctx.ReferralsTrees
                    .Include(t => t.Parent)
                    .Where(t => t.Parent.Username.Equals(searchTerm) || t.Parent.FirstName.Equals(searchTerm))
                    .Include(t => t.Child)
                    .ThenInclude(u => u.ReferralsTreeParents)
                    .ThenInclude(t => t.Child)
                   .Select(t => t.Child);
            
            IEnumerable<User> temp;
            try
            {
                temp = (await Pager<User>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize)).ToList();
            }
            catch
            {
                var keySelector = ReferralPager.GetSelector(sortParam);
                if (sortOrder == "asc")
                    query = query.OrderBy(keySelector);
                else
                    query = query.OrderByDescending(keySelector);
                temp = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return MapRefToDto(temp);
        }
    }
}
