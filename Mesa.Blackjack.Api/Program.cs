using Mesa.Blackjack.Data;
using Mesa.Blackjack.Handlers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.AspNetCore.Builder;
using Mesa.Blackjack.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuración para el DbContext
builder.Services.AddDbContext<BlackJackContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//MEDIATR
builder.Services.AddMediatR(cf =>
{
    cf.RegisterServicesFromAssembly(typeof(DummyAccesor).Assembly);
});

//automapper
builder.Services.AddAutoMapper(typeof(DummyAccesor).Assembly);


//repo
builder.Services.AddScoped<IBlackJackRepository, BlackJackRepsository>();
builder.Services.AddScoped<IGameRequestBackJackRepository, GameRequestBackJackRepository>();
builder.Services.AddSignalR();



var app = builder.Build();
app.MapHub<MiClaseSignalR>("/signalR");

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
