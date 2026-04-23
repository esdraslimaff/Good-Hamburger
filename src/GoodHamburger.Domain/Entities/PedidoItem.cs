using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities
{
    public class PedidoItem : BaseEntity
    {
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public TipoItem Tipo { get; private set; }
        protected PedidoItem() { }

        public PedidoItem(Item item)
        {   
            ProdutoId = item.Id;
            Nome = item.Nome;
            PrecoUnitario = item.PrecoUnitario;
            Tipo = item.Tipo;
        }
    }
}
