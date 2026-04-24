using GoodHamburger.BlazorWasm;
using GoodHamburger.BlazorWasm.Services;
using GoodHamburger.BlazorWasm.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//TO-DO: Centralizar D.I
builder.Services.AddScoped<ICardapioService, CardapioService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPromocaoService, PromocaoService>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:8080")
});

await builder.Build().RunAsync();