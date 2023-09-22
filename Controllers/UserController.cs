using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using web_panel_api.Dto;
using web_panel_api.Mapper;
using web_panel_api.Models;
using web_panel_api.Services;

namespace web_panel_api.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPresenter _prst;

        public UserController(IMapper mapper, IPresenter prst) { _mapper = mapper; _prst = prst; }
        [HttpGet()]
        public async Task<IEnumerable<GetUserDto>> GetUsers(int page, int pageSize, string? searchTerm, string sortParam, string sortOrder)
        {
            var ctx = new clientContext();
            IQueryable<User> query = ctx.Users.AsQueryable();
            if(!string.IsNullOrEmpty(searchTerm))
                query = query.Where(u => 
                (u.Username.Contains(searchTerm) || u.FirstName.Contains(searchTerm)));

            var result =  await Pager<User>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
            return _mapper.Map<IEnumerable<GetUserDto>>(result);
        }
        [HttpGet("demo")]
        public async Task<IEnumerable<GetUserDto>> GetUserForDemoPeriod(int page, int pageSize, string sortParam, string sortOrder, string? searchTerm)
        {
            var ctx = new clientContext();
            var query = ctx.Users.Where(u => u.IsFree == 0);
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(u =>
                (u.Username.Contains(searchTerm) || u.FirstName.Contains(searchTerm)));
           
            var result =  await Pager<User>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
            return _mapper.Map<IEnumerable<GetUserDto>>(result);
        }
        [HttpGet("referrals")]
        public async Task<IEnumerable<GetReferralDto>> GetReferralsForUser(string? searchTerm, int page, int pageSize, string sortParam, string sortOrder)
        {
            
            var ctx = new clientContext();
            var query = ctx.Users.AsQueryable();
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
           
               
            var result = new List<GetReferralDto>();
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
            foreach (var user in temp)
            {
                int activeAmount = 0;
                int notActive = 0;
                foreach (var tree in user.ReferralsTreeParents) 
                {
                    if(tree.Child.Status == 1)
                        activeAmount++;
                    else if(tree.Child.Status == 0)
                        notActive++;
                }
                var temporary = _mapper.Map<GetReferralDto>(user);
                temporary.Active = activeAmount;
                temporary.NotActive = notActive;
                result.Add(temporary);
            }
            return result;
        }
        [HttpPut]
        public async Task<IActionResult> ResolveActiveUsers(List<int> userIds)
        {
            var ctx = new clientContext();
            foreach(var id in userIds)
            {
                var user = await ctx.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new ArgumentNullException();
                user.Status = 1;
                ctx.SaveChanges();
            }
            return NoContent();
        }
        [HttpPut("demo")]
        public async Task<IActionResult> ResolveFreeSub(List<ResolveFreeSubDto> info)
        {
            var ctx = new clientContext();
            foreach (var item in info)
            {
                var userDatabase = await ctx.Users.FirstOrDefaultAsync(u => u.Id == item.Id);
                if (userDatabase == null)
                    throw new ArgumentNullException(nameof(User));
                var userTarif = new UsersTariff() { UserId =  item.Id, Duration = item.Duration };
                userTarif.Status = 0;
                userTarif.CreatedAt = DateTime.Now;
                userDatabase.IsFree = 1;
                await ctx.UsersTariffs.AddAsync(userTarif);
            }
            await ctx.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost("report")]
        public async Task<IActionResult> GetExcelReport(DateDto info)
        {
            var ctx = new clientContext();
            var query = await ctx.Users.Where(u => u.CreatedAt >= info.FirstTime && u.CreatedAt <= info.LastTime).ToListAsync();
            var dto = _mapper.Map<List<GetUserDto>>(query);
            var result = await _prst.GenerateFileBytes<GetUserDto>(dto);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "file.xlsx");
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto, int id)
        {
            var ctx = new clientContext();
            var userDatabase = await ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
            _mapper.Map(dto, userDatabase);
            await ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
