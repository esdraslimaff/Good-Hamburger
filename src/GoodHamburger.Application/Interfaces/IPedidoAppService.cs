using GoodHamburger.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Interfaces
{
    public interface IPedidoAppService
    {
        Task<PedidoResponse> CriarPedidoAsync(PedidoRequest request);
        Task<IEnumerable<PedidoResponse>> ListarTodosAsync();
        Task<PedidoResponse?> ObterPorIdAsync(Guid id);
        Task RemoverAsync(Guid id);
        Task AtualizarPedidoAsync(Guid id, PedidoRequest request);
    }
}
