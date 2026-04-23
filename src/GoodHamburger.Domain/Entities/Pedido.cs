using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Entities
{
    public class Pedido : BaseEntity
    {
        private readonly List<PedidoItem> _itens = new();
        public IReadOnlyCollection<PedidoItem> Itens => _itens.AsReadOnly();
        public decimal Subtotal { get; private set; }
        public decimal DescontoPercentual { get; private set; }
        public decimal ValorDesconto { get; private set; }
        public decimal TotalFinal { get; private set; }
        public Guid? PromocaoId { get; private set; }
        public Pedido() : base() { }

        public void AdicionarProduto(Item produto)
        {
            if (_itens.Any(i => i.Tipo == produto.Tipo))
                throw new DomainException($"O pedido já contém um item do tipo {produto.Tipo}.");

            if (_itens.Count >= 3)
                throw new DomainException("O pedido já atingiu o limite máximo de 3 itens.");

            _itens.Add(new PedidoItem(produto));

            DescontoPercentual = 0;
            RecalcularTotais();
        }

        public void RemoverProduto(Guid produtoId)
        {
            var item = _itens.FirstOrDefault(i => i.Id == produtoId);
            if (item != null)
            {
                _itens.Remove(item);

                DescontoPercentual = 0;
                RecalcularTotais();
            }
        }

        private void RecalcularTotais()
        {
            Subtotal = _itens.Sum(i => i.PrecoUnitario);
            ValorDesconto = Subtotal * DescontoPercentual;
            TotalFinal = Subtotal - ValorDesconto;

            RegistrarAlteracao();
        }

        public void AplicarPromocoes(IEnumerable<Promocao> promocoesDisponiveis)
        {
            var tiposNoPedido = _itens.Select(i => i.Tipo).ToList();

            var melhorPromocao = promocoesDisponiveis
                .Where(p => p.ContemTodosRequisitos(tiposNoPedido) && p.Requisitos.Count == tiposNoPedido.Count)
                .MaxBy(p => p.Percentual);

            if (melhorPromocao != null)
            {
                DescontoPercentual = melhorPromocao.Percentual;
                PromocaoId = melhorPromocao.Id;
            }
            else
            {
                
                DescontoPercentual = 0;
                ValorDesconto = 0;
                PromocaoId = null;
            }

            RecalcularTotais();
        }
    }
}