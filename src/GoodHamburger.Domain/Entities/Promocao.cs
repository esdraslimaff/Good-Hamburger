using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entities
{
    public class Promocao : BaseEntity
    {
        public string? Nome { get; private set; }
        public decimal Percentual { get; private set; }
        public bool Ativo { get; private set; }

        private readonly List<PromocaoItem> _requisitos = new();
        public IReadOnlyCollection<PromocaoItem> Requisitos => _requisitos.AsReadOnly();

        protected Promocao() { }

        public Promocao(string nome, decimal percentual)
        {
            Nome = nome;
            Percentual = percentual;
            Ativo = true;
        }

        public bool SeAplica(List<TipoItem> itensPedido) 
        {
            var requisitos = _requisitos.Select(r => r.TipoItem);
            return requisitos.All(r => itensPedido.Contains(r)); 
        }

    }
}
