using GoodHamburger.BlazorWasm;
using GoodHamburger.BlazorWasm.Security;
using GoodHamburger.BlazorWasm.Services;
using GoodHamburger.BlazorWasm.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//TO-DO: Centralizar D.I
builder.Services.AddScoped<ICardapioService, CardapioService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPromocaoService, PromocaoService>();
builder.Services.AddScoped<CustomAuthorizationHandler>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddHttpClient("GoodHamburgerAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:8080"); // A URL da API OU DOCKER
}).AddHttpMessageHandler<CustomAuthorizationHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("GoodHamburgerAPI"));

await builder.Build().RunAsync();