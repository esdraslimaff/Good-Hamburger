using GoodHamburger.BlazorWasm.Services.Interfaces;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GoodHamburger.BlazorWasm.Pages
{
    public partial class Pedidos
    {
        [Inject] public IPedidoService PedidoService { get; set; }
        [Inject] public NavigationManager Nav { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

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

        protected void IrParaEditar(Guid id) => Nav.NavigateTo($"/pedidos/editar/{id}");
        protected async Task ConfirmarExclusao(Guid id)
        {
            var confirmado = await JS.InvokeAsync<bool>("confirm", "Deseja realmente excluir este pedido?");

            if (confirmado)
            {
                await PedidoService.DeletarAsync(id);
                await CarregarPedidos();
            }
        }
        //TO-DO: SUBSTITUIR ESSE JS POR MODAL
    }
}
