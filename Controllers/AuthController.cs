using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using web_panel_api.Dto;
using web_panel_api.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace web_panel_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppOptions _opt;

        public AuthController(AppOptions opt) { _opt = opt; }
        [HttpPost]
        public IActionResult GetAuth(AuthDto authDto)
        {
            if (authDto.Password != _opt.AdminPassword || authDto.Login != _opt.AdminLogin)
                return StatusCode(401);
            var claims = new List<Claim> { new("Login", authDto.Login), new(ClaimTypes.Role, "admin") };
            var jwt = new JwtSecurityToken
                (claims: claims,
                expires: DateTime.Now.AddHours(5), 
                signingCredentials: new SigningCredentials(_opt.SymmetricKey, SecurityAlgorithms.HmacSha256));
            var jwtHandler = new JwtSecurityTokenHandler();
            return Ok(new { token = jwtHandler.WriteToken(jwt) });
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult CheckAuth() => Ok();
    }
}
