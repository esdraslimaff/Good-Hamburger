using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Shared.DTOs
{
    public class PedidoRequest
    {
        public List<Guid> ItensIds { get; set; } = new();
    }
}
