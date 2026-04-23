using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GoodHamburger.BlazorWasm.Pages
{
    public partial class Promocao
    {
        [Inject] public IPromocaoService PromocaoService { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        protected List<PromocaoDto> Promocoes { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CarregarPromocoes();
        }

        private async Task CarregarPromocoes()
        {
            try
            {
                Promocoes = await PromocaoService.GetPromocoesAsync();
            }
            catch (Exception)
            {
                Nav.NavigateTo("/");
            }
        }

        protected async Task AlternarStatusPromocao(PromocaoDto promo)
        {
            try
            {
                await PromocaoService.AlternarStatusAsync(promo.Id);
                promo.Ativo = !promo.Ativo;

                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar status: {ex.Message}");
            }
        }
    }
}