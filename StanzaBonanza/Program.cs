using Microsoft.EntityFrameworkCore;
using StanzaBonanza.DataAccess.DbContexts;
using StanzaBonanza.DataAccess.Repositories;
using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Services;
using StanzaBonanza.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPoemRepository, PoemRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPoem_AuthorRepository, Poem_AuthorRepository>();

builder.Services.AddScoped<IPoemAuthorJoinService, PoemAuthorJoinService>();

builder.Services.AddControllers()
    .AddJsonOptions(config => config.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
