using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using web_panel_api.Dto;
using web_panel_api.Mapper;
using web_panel_api.Models;
using web_panel_api.Services;
using web_panel_api.Services.Referral;

namespace web_panel_api.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPresenter _prst;
        private readonly IReferralService _refSrvc;

        public UserController(IMapper mapper, IPresenter prst, IReferralService refServc) { _mapper = mapper; _prst = prst; _refSrvc = refServc; }
        [HttpGet()]
        public async Task<IEnumerable<GetUserDto>> GetUsers(int page, int pageSize, string? searchTerm, string sortParam, string sortOrder, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                IQueryable<User> query = ctx.Users.AsNoTracking().Include(u => u.UsersKeys).AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                    query = query.Where(u =>
                    (u.Username != null && u.Username.Contains(searchTerm)) || (u.FirstName != null && u.FirstName.Contains(searchTerm)));
                IEnumerable<User> result;
                try
                {
                    result = await Pager<User>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
                }
                catch
                {
                    result = await UserPager.GetPagedUserForVpnService(query, sortParam, sortOrder, page, pageSize);
                }
                var temporary = _mapper.Map<IEnumerable<GetUserDto>>(result);
                foreach (var user in temporary)
                    user.UsersKeys = user.UsersKeys.Where(uk => uk.Status == 1).ToList();
                return temporary;
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                IQueryable<web_panel_api.Models.god_eyes.User> query = ctx.Users.AsNoTracking().AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                    query = query.Where(u =>
                    (u.Username != null && u.Username.Contains(searchTerm)) || (u.FirstName != null && u.FirstName.Contains(searchTerm)));
                IEnumerable<web_panel_api.Models.god_eyes.User> result;
                    result = await Pager<web_panel_api.Models.god_eyes.User>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
                var temporary = _mapper.Map<IEnumerable<GetUserDto>>(result);
                return temporary;
            }
        }
        [HttpGet("demo")]
        public async Task<IEnumerable<GetUserDto>> GetUserForDemoPeriod(int page, int pageSize, string sortParam, string sortOrder, string? searchTerm)
        {
            var ctx = new clientContext();
            var query = ctx.Users.AsNoTracking().Where(u => u.IsFree == 0);
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(u =>
                (u.Username != null && u.Username.Contains(searchTerm)) || (u.FirstName != null && u.FirstName.Contains(searchTerm)));
            IEnumerable<User> result;
            try
            {
                result = await Pager<User>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
            }
            catch
            {
                result = await UserPager.GetPagedUserForVpnService(query, sortParam, sortOrder, page, pageSize);
            }
            return _mapper.Map<IEnumerable<GetUserDto>>(result);
        }
        [HttpGet("referrals")]
        public async Task<IEnumerable<GetReferralDto>> GetReferralsForUser(string? searchTerm, int page,
            int pageSize, string sortParam, string sortOrder, string project)
        {
            var result = await _refSrvc.GetReferralsForUser(searchTerm, page, pageSize, sortParam, sortOrder, project);
            return result;
        }


        [HttpGet("referrals/father")]
        public async Task<object> GetFatherForRef(string term, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                var parent = await ctx.Users.Where(u => u.FirstName == term || u.Username == term)
                    .Include(u => u.ReferralsTreeChildren)
                    .SelectMany(u => u.ReferralsTreeChildren)
                    .Include(t => t.Parent)
                    .Select(t => t.Parent.Username)
                    .FirstOrDefaultAsync();
                return new { term = parent ?? "" };
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var parent = await ctx.Users.Where(u => u.FirstName == term || u.Username == term)
                    .Include(u => u.ReferralChild)
                    .Select(u => u.ReferralChild)
                    .Include(t => t.Parent)
                    .Where(t => t.Parent != null)
                    .Select(t => t.Parent.Username)
                    .FirstOrDefaultAsync();
                return new { term = parent ?? "" };
            }
        }
        [HttpPut]
        public async Task<IActionResult> ResolveActiveUsers(List<int> userIds, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                foreach (var id in userIds)
                {
                    var user = await ctx.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new ArgumentNullException();
                    user.Status = 1;
                  
                }
                ctx.SaveChanges();
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                foreach (var id in userIds)
                {
                    var user = await ctx.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new ArgumentNullException();
                    user.Status = true;
                }
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
                var userTarif = new UsersTariff() { UserId = item.Id, Duration = item.Duration };
                userTarif.Status = 0;
                userTarif.CreatedAt = DateTime.Now;
                userDatabase.IsFree = 1;
                await ctx.UsersTariffs.AddAsync(userTarif);
            }
            await ctx.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost("report")]
        public async Task<IActionResult> GetExcelReport(DateDto info, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                var query = await ctx.Users.AsNoTracking().Where(u => u.CreatedAt >= info.FirstTime && u.CreatedAt <= info.LastTime).ToListAsync();
                var dto = _mapper.Map<List<GetUserDto>>(query);
                var result = await _prst.GenerateFileBytes<GetUserDto>(dto);
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "file.xlsx");
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var query = await ctx.Users.AsNoTracking().Where(u => u.CreatedAt >= info.FirstTime && u.CreatedAt <= info.LastTime).ToListAsync();
                var dto = _mapper.Map<List<GetUserDto>>(query);
                var result = await _prst.GenerateFileBytes<GetUserDto>(dto);
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "file.xlsx");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto, int id, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                var userDatabase = await ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
                _mapper.Map(dto, userDatabase);
                await ctx.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var userDatabase = await ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
                _mapper.Map(dto, userDatabase);
                await ctx.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}
