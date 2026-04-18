using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Interfaces
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        Task<Pedido?> GetPedidoComItensAsync(Guid id);
    }
}
