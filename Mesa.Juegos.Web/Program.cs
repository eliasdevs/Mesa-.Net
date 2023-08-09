using Mesa.Juegos.Modules.BlackJack;
using Mesa.Juegos.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//capturo la configuracion del servidor de tiempo real
ConfigRealTime? configRealTime= builder.Configuration.GetSection(nameof(ConfigRealTime)).Get<ConfigRealTime>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
