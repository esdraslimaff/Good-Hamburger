using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using GoodHamburger.BlazorWasm.Security;

namespace GoodHamburger.BlazorWasm.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] public NavigationManager Nav { get; set; }
        [Inject] public IJSRuntime JS { get; set; }
        [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }

        protected LoginRequestDto loginRequest = new();
        protected string MensagemErro;
        protected bool Carregando = false;

        protected async Task FazerLogin()
        {
            Carregando = true;
            MensagemErro = string.Empty;

            try
            {
                var response = await Http.PostAsJsonAsync("/api/Auth/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<TokenResponse>();

                    await JS.InvokeVoidAsync("localStorage.setItem", "authToken", resultado.Token);

                    var authProvider = (CustomAuthStateProvider)AuthStateProvider;
                    authProvider.MarcarUsuarioComoLogado(resultado.Token);

                    Nav.NavigateTo("/pedidos");
                }
                else
                {
                    MensagemErro = "E-mail ou senha incorretos.";
                }
            }
            catch (Exception)
            {
                MensagemErro = "Erro ao conectar com o servidor.";
            }
            finally
            {
                Carregando = false;
            }
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}