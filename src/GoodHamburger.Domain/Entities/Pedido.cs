using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }

        private readonly List<Item> _itens = new();
        public IReadOnlyCollection<Item> Itens => _itens.AsReadOnly();

        public Pedido()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
        }

        public void AdicionarItem(Item item)
        {
            if (_itens.Any(i => i.Tipo == item.Tipo))
            {
                throw new DomainException($"O pedido já contém um item do tipo {item.Tipo}.");
            }

            _itens.Add(item);
        }

        public decimal CalcularSubtotal() => _itens.Sum(i => i.Preco);

        public decimal CalcularDesconto()
        {
            var temSanduiche = _itens.Any(i => i.Tipo == TipoItem.Sanduiche);
            var temBatata = _itens.Any(i => i.Tipo == TipoItem.Acompanhamento);
            var temRefri = _itens.Any(i => i.Tipo == TipoItem.Bebida);

            if (temSanduiche && temBatata && temRefri) return 0.20m;
            if (temSanduiche && temRefri) return 0.15m;            
            if (temSanduiche && temBatata) return 0.10m; 

            return 0m;
        }

        public decimal CalcularTotalFinal()
        {
            var subtotal = CalcularSubtotal();
            var desconto = subtotal * CalcularDesconto();
            return subtotal - desconto;
        }
    }
}
