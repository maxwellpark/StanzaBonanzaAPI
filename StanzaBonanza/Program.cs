using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StanzaBonanza.API.Filters;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.DataAccess.Repositories;
using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Services;
using StanzaBonanza.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPoemRepository, PoemRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPoem_AuthorRepository, Poem_AuthorRepository>();

builder.Services.AddScoped<IPoemAuthorJoinService, PoemAuthorJoinService>();

// Add API Key authentication via filter but bypass when not in production 
if (builder.Environment.IsProduction())
{
    builder.Services.AddControllers(options => options.Filters.Add<ApiKeyAttribute>());
}
else
{
    builder.Services.AddControllers();
}

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

app.UseAuthorization();

app.MapControllers();

app.Run();
