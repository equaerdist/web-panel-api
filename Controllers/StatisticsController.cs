using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using web_panel_api.Dto;
using web_panel_api.Models;
using web_panel_api.Services;
using web_panel_api.Services.Statictics;

namespace web_panel_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStatisticsService _srvc;

        public StatisticsController(IMapper mapper, IStatisticsService srvc) { _mapper = mapper; _srvc = srvc; }

        [HttpPost]
        public async Task<GetStaticticsDto> GetStats(DateDto dates, string group, string offset, string project)
        {
           return await _srvc.GetStats(dates, group, offset, project);
        }

    }
}
