using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using web_panel_api.Dto;
using web_panel_api.Models;

namespace web_panel_api.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IMapper _mpr;

        public SettingsController(IMapper mpr) { _mpr = mpr; }
        [HttpGet]
        public async Task<Setting?> GetSettings()
        {
            var ctx = new clientContext();
            var settings = await ctx.Settings.FirstOrDefaultAsync();
            return settings;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSettings(SettingDto newSet)
        {
            var ctx = new clientContext();
            var setDb = await ctx.Settings.FirstOrDefaultAsync(s => s.Id == newSet.Id) ?? throw new ArgumentException("Сущности не существует");
            _mpr.Map(newSet, setDb);
            await ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
