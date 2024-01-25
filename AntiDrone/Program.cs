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
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddDistributedMemoryCache();

// 세션 사용하기 위해 추가
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); /* 지속 시간 */
    options.Cookie.Name = "AntiDroneSession"; /* 세션명 */
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

// 파이프라인에서 세션 사용, 미들웨어
app.UseSession();

app.MapControllers();

app.Run();