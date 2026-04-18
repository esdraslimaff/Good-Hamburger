using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Shared.DTOs
{
    public class PedidoResponse
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<ItemCardapioDto> Itens { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal DescontoPercentual { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal TotalFinal { get; set; }
    }
}
