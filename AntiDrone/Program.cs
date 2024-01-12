using AntiDrone.Data;
using AntiDrone.Models.Systems.DroneControl;
using AntiDrone.Services;
using AntiDrone.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AntiDroneContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AntiDroneContext") ?? throw new InvalidOperationException("Connection string 'AntiDroneContext' not found.")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWhitelistService, WhitelistService>();

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