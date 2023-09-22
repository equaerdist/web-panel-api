using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data;
using System.Runtime.CompilerServices;
using web_panel_api.Dto;
using web_panel_api.Models;
using web_panel_api.Services;
using WebApplication5.Tools;

namespace web_panel_api.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TarifController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;

        public TarifController(IMapper mapper, ITokenGenerator generator) { _mapper = mapper; _tokenGenerator = generator; }
        [HttpGet]
        public async Task<IEnumerable<Tariff>> GetTariffs(int page, int pageSize, string? searchTerm, string sortParam, string sortOrder)
        {
            var ctx = new clientContext();
            var query = ctx.Tariffs.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(t => t.TariffName.Contains(searchTerm));
            return await Pager<Tariff>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
        }
        [HttpGet("promocodes")]
        public async Task<IEnumerable<Promocode>> GetPromocodes(int page, int pageSize, string? searchTerm, string sortParam, string sortOrder)
        {
            var ctx = new clientContext();
            var query = ctx.Promocodes.Include(p => p.Tariff).Where(p => p.UserId == null);
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(p => p.ValueCode.Contains(searchTerm));
            return await Pager<Promocode>.GetPagedEnumerable(query, sortParam, sortOrder, page, pageSize);
        }
        [HttpDelete("promocodes/{id:int}")]
        public async Task<IActionResult> DeletePromocode(int id)
        {
            var ctx = new clientContext();
            var promoDatabase = await ctx.Promocodes.FirstOrDefaultAsync(p => p.Id == id);
            if (promoDatabase == null)
                throw new ArgumentNullException("Такого промокода не существует");
            ctx.Promocodes.Remove(promoDatabase);
            await ctx.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost("promocodes")]
        public async Task<Promocode> AddPromocode(AddPromocodeDto info)
        {
            var ctx = new clientContext();
            var tariffDatabase = await ctx.Tariffs.FirstOrDefaultAsync(t => t.TariffName == info.TariffName);
            if (tariffDatabase == null)
                throw new ArgumentNullException("Такого тарифа не существует");
            var promocode = new Promocode();
            promocode.Status = 1;
            promocode.Tariff = tariffDatabase;
            int i = 0;
            while(true)
            {
                if(i == 100)
                    throw new InvalidOperationException("Токен не может быть сгенерирован. Он совпал 100 раз. Следует очистить старые промокоды");
                var token = _tokenGenerator.GenerateToken();
                var similarTokens = await ctx.Promocodes.FirstOrDefaultAsync(p => p.ValueCode ==  token);
                if (similarTokens == null)
                {
                    promocode.ValueCode = token;
                    break;
                }
                i++;
            }
            promocode.CreateAt = DateTime.Now;
            await ctx.Promocodes.AddAsync(promocode);
            await ctx.SaveChangesAsync();
            return promocode;
        }
        [HttpPost]
        public async Task<Tariff> CreateTariff(AddTariffDto info)
        {
            var ctx = new clientContext();
            var newTarif = _mapper.Map<Tariff>(info);
            newTarif.CreatedAt = DateTime.Now;
            await ctx.Tariffs.AddAsync(newTarif);
            await ctx.SaveChangesAsync();
            return newTarif;
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTariff(int id)
        {
            var ctx = new clientContext();
            var tariffDatabase = await ctx.Tariffs.FirstOrDefaultAsync(t => t.Id == id);
            if (tariffDatabase is null)
                throw new ArgumentException("Такого тарифа не существует");
            ctx.Tariffs.Remove(tariffDatabase);
            await ctx.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTariff(int id, AddTariffDto dto)
        {
            var ctx = new clientContext();
            var tariffDb = await ctx.Tariffs.FirstOrDefaultAsync(t => t.Id == id);
            if (tariffDb is null)
                throw new ArgumentException("Такого тарифа не существует");
            _mapper.Map(dto, tariffDb);
            await ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
