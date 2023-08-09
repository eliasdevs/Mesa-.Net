using Mesa.RealTime.Project.Hubs;
using Mesa_SV.BlackJack;
using Mesa_SV.BlackJack.Config;
using Microsoft.AspNetCore.ResponseCompression;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfiguracionBlackJack? configBlackJack = builder.Configuration.GetSection(nameof(ConfiguracionBlackJack)).Get<ConfiguracionBlackJack>();

if (configBlackJack != null)
    builder.Services.AddRefitClient<IBlackJackJackSdk>()
    .ConfigureHttpClient(x => x.BaseAddress = new Uri(configBlackJack.UrlBase));
//.AddHttpMessageHandler(x => new BearerHttpClientHandler(x.GetService<IHttpContextAccessor>()));

// habilita las cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7006")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddSignalR();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.MapHub<BlackJackHub>("/BlackJackRealTime");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
