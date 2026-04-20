using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GoodHamburger.BlazorWasm.Pages
{
    public partial class Pedidos
    {
        [Inject] public IPedidoService PedidoService { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        protected List<PedidoResponse> ListaPedidos;

        protected override async Task OnInitializedAsync()
        {
            await CarregarPedidos();
        }

        protected async Task CarregarPedidos()
        {
            ListaPedidos = await PedidoService.GetTodosAsync();
        }
        protected void IrParaNovoPedido() => Nav.NavigateTo("/");

        protected void VerDetalhes(Guid id) => Nav.NavigateTo($"/pedidos/{id}");

        protected async Task ConfirmarExclusao(Guid id)
        {
            return;
            //TO-DO: CONFIGURA EXCLUSÃO
            //await PedidoService.DeletarAsync(id);
            //await CarregarPedidos();
        }
    }
}
