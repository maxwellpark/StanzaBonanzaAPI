using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StanzaBonanza.API.Filters;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.DataAccess.UnitOfWork;
using StanzaBonanza.Services;
using StanzaBonanza.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddLogging();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPoem_AuthorService, Poem_AuthorService>();

builder.Services.AddTransient<ITokenService, JwtTokenService>();

// Add API Key authentication via filter but bypass when not in production 
if (builder.Environment.IsProduction())
{
    builder.Services.AddControllers(options => options.Filters.Add<ApiKeyAttribute>());
}
else
{
    builder.Services.AddControllers();
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://accounts.google.com";
    // Client ID 
    options.Audience = "836340696024-i8v368it0h43bvhuhgikm8kecubkafaa.apps.googleusercontent.com";
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stanza Bonanza", Version = "v1" });
    c.OperationFilter<ApiKeyOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
// Apply Cookie middleware first in the pipeline 
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireAuthorization();
});

app.MapControllers();

app.Run();
