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
            var resultado = await JS.InvokeAsync<SwalResult>("Swal.fire", new
            {
                title = "Deseja realmente excluir este pedido?",
                text = "Essa ação não poderá ser desfeita!",
                icon = "warning",
                showCancelButton = true,
                confirmButtonColor = "#dc3545",
                cancelButtonColor = "#6c757d",
                confirmButtonText = "Sim, deletar",
                cancelButtonText = "Cancelar"
            });

            if (resultado.IsConfirmed)
            {
                await PedidoService.DeletarAsync(id);
                await CarregarPedidos();

                await JS.InvokeVoidAsync("Swal.fire", "Deletado!", "O pedido foi excluído com sucesso.", "success");
            }
        }
    }

    public class SwalResult
    {
        public bool IsConfirmed { get; set; }
    }
}