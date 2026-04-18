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
        private readonly List<Item> _itens = new();
        public IReadOnlyCollection<Item> Itens => _itens.AsReadOnly();

        public Pedido() : base() { }

        public void AdicionarItem(Item item)
        {
            if (_itens.Any(i => i.Tipo == item.Tipo))
            {
                throw new DomainException($"O pedido já contém um item do tipo {item.Tipo}.");
            }

            _itens.Add(item);
            RegistrarAlteracao();
        }

        public decimal CalcularSubtotal() => _itens.Sum(i => i.Preco);

        public decimal CalcularDescontoPercentual()
        {
            var temSanduiche = _itens.Any(i => i.Tipo == TipoItem.Sanduiche);
            var temBatata = _itens.Any(i => i.Tipo == TipoItem.Acompanhamento);
            var temRefri = _itens.Any(i => i.Tipo == TipoItem.Bebida);

            if (temSanduiche && temBatata && temRefri) return 0.20m;
            if (temSanduiche && temRefri) return 0.15m;
            if (temSanduiche && temBatata) return 0.10m;

            return 0m;
        }

        public decimal CalcularValorDesconto() => CalcularSubtotal() * CalcularDescontoPercentual();

        public decimal CalcularTotalFinal() => CalcularSubtotal() - CalcularValorDesconto();
    }
}
