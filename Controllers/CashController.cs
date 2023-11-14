using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using web_panel_api.Dto;
using web_panel_api.Models;
using web_panel_api.Services;

namespace web_panel_api.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CashController : ControllerBase
    {
        private readonly IMapper _mapper;

        public CashController(IMapper mapper) { _mapper = mapper; }
        [HttpGet("given")]
        public async Task<IEnumerable<GetPayHistoryDto>> GetGivenMoney(int page, int pageSize, string sortParam, string sortOrder, string? searchTerm, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                var query = ctx.PayHistories.Include(ph => ph.User)
                    .Where(ph => ph.PaymentType == "output" && ph.StatusPay == 1 
                    && ph.PaymentMethod == "balance");
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query
                        .Where
                        (ph => ph.User != null &&
                        ((ph.User.Username != null && ph.User.Username.Contains(searchTerm))
                        || (ph.User.FirstName != null && ph.User.FirstName.Contains(searchTerm))));
                }
                var result = await Pager<PayHistory>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
                return _mapper.Map<IEnumerable<GetPayHistoryDto>>(result);
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var query = ctx.Pays.Include(ph => ph.User)
                    .Where(ph => ph.Type == "OUTPUT" && ph.Status == 1 && ph.Method == "BALANCE");
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query
                        .Where
                        (ph => ph.User != null &&
                        ((ph.User.Username != null && ph.User.Username.Contains(searchTerm))
                        || (ph.User.FirstName != null && ph.User.FirstName.Contains(searchTerm))));
                }
                var result = await Pager<web_panel_api.Models.god_eyes.Pay>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
                return _mapper.Map<IEnumerable<GetPayHistoryDto>>(result);
            }
        }
        [HttpGet("request")]
        public async Task< IEnumerable<GetPayHistoryDto>> GetRequestToExit(int page, int pageSize, string? searchTerm, string sortParam, string sortOrder, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                var query = ctx.PayHistories.Include(ph => ph.User)
                    .Where(ph => ph.PaymentType == "output" && ph.StatusPay == 0 && ph.PaymentMethod == "balance");
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query
                        .Where(ph => ph.User != null && ((ph.User.Username != null && ph.User.Username.Contains(searchTerm)) ||
                        (ph.User.FirstName != null && ph.User.FirstName.Contains(searchTerm))));
                }
                var result = await Pager<PayHistory>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
                return _mapper.Map<IEnumerable<GetPayHistoryDto>>(result);
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var query = ctx.Pays.Include(ph => ph.User).Where(ph => ph.Type == "OUTPUT" && ph.Status == 0 && ph.Method == "BALANCE");
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query
                        .Where(ph => ph.User != null && ((ph.User.Username != null && ph.User.Username.Contains(searchTerm)) ||
                        (ph.User.FirstName != null && ph.User.FirstName.Contains(searchTerm))));
                }
                var result = await Pager<web_panel_api.Models.god_eyes.Pay>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
                return _mapper.Map<IEnumerable<GetPayHistoryDto>>(result);
            }
        }
        [HttpPut("request")]
        public async Task<IActionResult> ConfirmRequests(ConfirmRequestDto request, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                foreach (var payRequest in request.PayRequests)
                {
                    var payRequestDatabase = await ctx.PayHistories
                        .FirstOrDefaultAsync(ph => ph.Id == payRequest.Id);
                    if (payRequestDatabase is null)
                        throw new ArgumentNullException(nameof(payRequestDatabase));
                    _mapper.Map(payRequest, payRequestDatabase);
                    await ctx.SaveChangesAsync();
                }
                
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                foreach (var payRequest in request.PayRequests)
                {
                    var payRequestDatabase = await ctx.Pays.FirstOrDefaultAsync(ph => ph.Id == payRequest.Id);
                    if (payRequestDatabase is null)
                        throw new ArgumentNullException(nameof(payRequestDatabase));
                    payRequestDatabase.Id = payRequest.Id;
                    await ctx.SaveChangesAsync();
                }
            }
            return NoContent();
        }
    }
}
