using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Shared.DTOs
{
    public record ItemCardapioDto(Guid Id, string Nome, decimal Preco, TipoItem Tipo);
}
