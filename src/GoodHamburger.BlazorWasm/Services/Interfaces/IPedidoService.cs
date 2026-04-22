using GoodHamburger.Shared.DTOs;

namespace GoodHamburger.BlazorWasm.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoResponse> CriarPedidoAsync(PedidoRequest request);
        Task<List<PedidoResponse>> GetTodosAsync();
        Task<PedidoResponse> GetPorIdAsync(Guid id);
        Task DeletarAsync(Guid id);
        Task AtualizarAsync(Guid id, PedidoRequest request);
    }
}
