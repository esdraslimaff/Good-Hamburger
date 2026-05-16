using GoodHamburger.Domain.Enums;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace GoodHamburger.BlazorWasm.Pages
{
    public partial class CadastrarUsuario : ComponentBase
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        protected UsuarioRequestDto novoUsuario = new()
        {
            Perfil = (int)TipoPerfil.Atendente
        };

        protected bool Salvando = false;
        protected string MensagemErro;
        protected string MensagemSucesso;

        protected async Task SalvarUsuario()
        {
            Salvando = true;
            MensagemErro = string.Empty;
            MensagemSucesso = string.Empty;

            try
            {
                var response = await Http.PostAsJsonAsync("/api/Usuarios", novoUsuario);

                if (response.IsSuccessStatusCode)
                {
                    MensagemSucesso = "Usuário cadastrado com sucesso!";
                    novoUsuario = new UsuarioRequestDto { Perfil = (int)TipoPerfil.Atendente };
                }
                else
                {
                    MensagemErro = "Erro ao cadastrar usuário. Verifique os dados e tente novamente.";
                }
            }
            catch (Exception ex)
            {
                MensagemErro = $"Erro de comunicação com o servidor: {ex.Message}";
            }
            finally
            {
                Salvando = false;
            }
        }

        protected void Voltar()
        {
            Nav.NavigateTo("/pedidos");
        }
    }
}