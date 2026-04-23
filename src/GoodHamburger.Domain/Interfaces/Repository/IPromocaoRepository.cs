using GoodHamburger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Interfaces.Repository
{
    public interface IPromocaoRepository:IBaseRepository<Promocao>
    {
        Task<IEnumerable<Promocao>> ObterTodasAtivasAsync();
        Task<Promocao?> BuscarPromocaoComRequisitosPorIdAsync(Guid id);
        Task<IEnumerable<Promocao>> ObterTodasPromocoesComRequisitosAsync();
    }
}
