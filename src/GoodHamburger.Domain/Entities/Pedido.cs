using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Pedido() : base() { }

        public void AdicionarProduto(Item produto)
        {
            if (_itens.Any(i => i.Tipo == produto.Tipo))
                throw new DomainException($"O pedido já contém um item do tipo {produto.Tipo}.");

            if (_itens.Count >= 3)
                throw new DomainException("O pedido já atingiu o limite máximo de 3 itens.");

            _itens.Add(new PedidoItem(produto));

            Subtotal = _itens.Sum(i => i.PrecoUnitario);

            RegistrarAlteracao();
        }

        public void ProcessarPedido(IEnumerable<Promocao> regrasAtivas)
        {
            DescontoPercentual = CalcularDescontoPercentual(regrasAtivas);
            ValorDesconto = Subtotal * DescontoPercentual;
            TotalFinal = Subtotal - ValorDesconto;

            RegistrarAlteracao();
        }

        private decimal CalcularDescontoPercentual(IEnumerable<Promocao> regrasAtivas)
        {
            var tiposNoPedido = _itens.Select(i => i.Tipo).ToList();

            var melhorRegra = regrasAtivas
                .Where(r => r.SeAplica(tiposNoPedido))
                .OrderByDescending(r => r.Percentual)
                .FirstOrDefault();

            return melhorRegra?.Percentual ?? 0m;
        }
    }
}
