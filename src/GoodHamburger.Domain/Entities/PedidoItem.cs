using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entities
{
    public class PedidoItem : BaseEntity
    {
        public Guid ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public int Quantidade { get; set; }
        public TipoItem Tipo { get; private set; }

        protected PedidoItem() { }

        public PedidoItem(Item item)
        {
            ProdutoId = item.Id;
            Nome = item.Nome;
            PrecoUnitario = item.Preco;
            Tipo = item.Tipo;
        }
    }
}
