using GoodHamburger.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Interfaces
{
    public interface IPromocaoService
    {
        Task<IEnumerable<PromocaoDto>> ObterAtivasAsync();
    }
}
