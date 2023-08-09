using Fluxor;
using Mesa.Juegos.State.Actions;
using Mesa.Juegos.State;
using Mesa.Juegos.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Mesa_SV.BlackJack.Config;
using Mesa.TimeReal.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//capturo la configuracion del servidor de tiempo real
ConfigRealTime? configRealTime = builder.Configuration.GetSection(nameof(ConfigRealTime)).Get<ConfigRealTime>();

if(configRealTime != null)
    builder.Services.AddSingleton(configRealTime);

builder.Services.AddScoped<IHubConnectionService, HubConnectionService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddFluxor(config =>
{
    config.ScanAssemblies(typeof(DummyMarker).Assembly).UseReduxDevTools();
});

builder.Services.AddScoped<DialogService>();

await builder.Build().RunAsync();
