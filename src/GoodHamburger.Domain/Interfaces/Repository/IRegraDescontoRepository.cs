using GoodHamburger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Interfaces.Repository
{
    public interface IRegraDescontoRepository
    {
        Task<IEnumerable<RegraDesconto>> ObterTodasAtivasAsync();
    }
}
