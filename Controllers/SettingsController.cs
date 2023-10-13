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
        public async Task<IActionResult> GetSettings(string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                var settings = await ctx.Settings.FirstOrDefaultAsync();
                return Ok(settings);
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var settings = await ctx.Settings.FirstOrDefaultAsync();
                return Ok(settings);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSettings(SettingDto newSet, string project)
        {
            if (project.Equals("poleteli_vpn"))
            {
                var ctx = new clientContext();
                var setDb = await ctx.Settings.FirstOrDefaultAsync(s => s.Id == newSet.Id) ?? throw new ArgumentException("Сущности не существует");
                _mpr.Map(newSet, setDb);
                await ctx.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var setDb = await ctx.Settings.FirstOrDefaultAsync(s => s.Id == newSet.Id) ?? throw new ArgumentException("Сущности не существует");
                _mpr.Map(newSet, setDb);
                await ctx.SaveChangesAsync();
                return NoContent();
            }
        }
        [HttpPost("message")]
        public async Task<IActionResult> AddMessageToBroadcast(SendMessageDto message, string project)
        {
            
            if(project.Equals("poleteli_vpn"))
            {
                var newMessage = new SendMessage();
                newMessage.Text = message.Text;
                newMessage.Type = message.Type ? "active" : "notactive";
                newMessage.DateCreate = DateTime.Now;
                newMessage.Send = 0;
                var ctx = new clientContext();
                await ctx.SendMessages.AddAsync(newMessage);
                await ctx.SaveChangesAsync();
            }
            else
            {
                var ctx = new web_panel_api.Models.god_eyes.headContext();
                var newMessage = new web_panel_api.Models.god_eyes.SendMessage();
                newMessage.Text = message.Text;
                newMessage.Type = message.Type ? "active" : "notactive";
                newMessage.DateCreate = DateTime.Now;
                newMessage.Send = 0;
                await ctx.SendMessages.AddAsync(newMessage);
                await ctx.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
