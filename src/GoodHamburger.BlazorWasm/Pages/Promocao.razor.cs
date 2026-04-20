using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GoodHamburger.BlazorWasm.Pages
{
    public partial class Promocao
    {
        [Inject] public IPromocaoService PromocaoService { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        protected List<PromocaoDto> Promocoes;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Promocoes = await PromocaoService.GetPromocoesAsync();
                Console.WriteLine("ababa");
            }
            catch (Exception)
            {
                Nav.NavigateTo("/");
            }
        }
    }
}
