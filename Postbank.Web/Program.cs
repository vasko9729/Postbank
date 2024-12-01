using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Postbank.Web;
using Postbank.Web.Client;
using Postbank.Web.Client.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient(nameof(DataClient), client =>
{
	client.BaseAddress = new Uri("https://localhost:7075");
});

builder.Services.AddScoped<IDataClient, DataClient>();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
