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
                    if (tree.Children.Status == 1)
                        activeAmount++;
                    else if (tree.Children.Status == 0)
                        notActive++;
                }
                var temporary = _mapper.Map<GetReferralDto>(user);
                temporary.UsersKeys = temporary.UsersKeys.Where(uk => uk.Status == 1).ToList();
                temporary.Active = activeAmount;
                temporary.NotActive = notActive;
                result.Add(temporary);
            }
            return result;
        }
        private List<GetReferralDto> MapRefToDto(IEnumerable<web_panel_api.Models.god_eyes.User> temp)
        {
            List<GetReferralDto> result = new();
            foreach (var user in temp)
            {
                int activeAmount = 0;
                int notActive = 0;
                foreach (var tree in user.ReferralParents)
                {
                    if (tree.Child.Status is true)
                        activeAmount++;
                    else if (tree.Child.Status is false)
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
        public async Task<IEnumerable<GetReferralDto>> GetReferralsForUser(string? searchTerm, int page, int pageSize, string sortParam, string sortOrder, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                var query = ctx.Users.AsQueryable();

                if (string.IsNullOrEmpty(searchTerm))
                    query = ctx.Users
                        .Include(u => u.ReferralsTreeChildren)
                        .Where(u => u.ReferralsTreeChildren.Count == 0)
                        .Include(u => u.ReferralsTreeParents)
                        .ThenInclude(t => t.Children)
                        .Include(u => u.UsersKeys);
                else
                    query = ctx.ReferralsTrees
                        .Include(t => t.Parent)
                        .Where(t => (t.Parent.Username != null && t.Parent.Username.Equals(searchTerm))
                        || (t.Parent.FirstName != null && t.Parent.FirstName.Equals(searchTerm)))
                        .Include(t => t.Children)
                        .ThenInclude(u => u.ReferralsTreeParents)
                        .ThenInclude(t => t.Children)
                        .Include(t => t.Children)
                        .ThenInclude(ch => ch.UsersKeys)
                       .Select(t => t.Children);
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
            else 
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var query = ctx.Users.AsQueryable();

                if (string.IsNullOrEmpty(searchTerm))
                    query = ctx.Users
                        .Include(u => u.ReferralChild)
                        .ThenInclude(r => r.Parent)
                        .Where(u => u.ReferralChild.Parent == null)
                        .Include(u => u.ReferralParents)
                        .ThenInclude(t => t.Child);
                else
                    query = ctx.Referrals
                        .Include(t => t.Parent)
                        .Where(t => t.Parent != null && 
                        ((t.Parent.Username != null && t.Parent.Username.Equals(searchTerm)) 
                        || (t.Parent.FirstName != null && t.Parent.FirstName.Equals(searchTerm))))
                        .Include(t => t.Child)
                        .ThenInclude(u => u.ReferralParents)
                        .ThenInclude(t => t.Child)
                       .Select(t => t.Child);
                IEnumerable<web_panel_api.Models.god_eyes.User> temp;
                try
                {
                    temp = (await Pager<web_panel_api.Models.god_eyes.User>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize)).ToList();
                }
                catch
                {
                    var keySelector = ReferralPager.GetSecondSelector(sortParam);
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
}
