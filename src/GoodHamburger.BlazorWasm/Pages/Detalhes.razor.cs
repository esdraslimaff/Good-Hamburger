using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GoodHamburger.BlazorWasm.Pages
{
    public partial class Detalhes
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] public IPedidoService PedidoService { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        protected PedidoResponse Pedido;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Pedido = await PedidoService.GetPorIdAsync(Id);
            }
            catch (Exception)
            {
                Nav.NavigateTo("/");
            }
        }

        protected void VoltarParaHome() => Nav.NavigateTo("/");
    }
}
