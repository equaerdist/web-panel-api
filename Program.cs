using web_panel_api;
using web_panel_api.Mapper;
using web_panel_api.Models;
using web_panel_api.Services;
using WebApplication5.Tools;
using Newtonsoft;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using web_panel_api.Services.Referral;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration.Get<AppOptions>(opt => opt.BindNonPublicProperties = true);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTime;
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateActor = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = config.SymmetricKey
    }
);
builder.Services.AddScoped<IReferralService, ReferralService>();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<clientContext>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IWalletReporter, WalletReporter>();
builder.Services.AddScoped<ITokenGenerator, TokenGeneratorByStringLib>();
builder.Services.AddScoped<IPresenter, ExcelPresenter>();
builder.Services.AddSingleton(config);
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(opt => opt
.AllowAnyHeader()
.AllowAnyMethod()
.AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
