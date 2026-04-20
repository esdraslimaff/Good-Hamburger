using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entities
{
    public class RegraDesconto : BaseEntity
    {
        public string Nome { get; private set; }
        public decimal Percentual { get; private set; }
        public bool Ativo { get; private set; }

        private readonly List<TipoItem> _requisitos = new();
        public IReadOnlyCollection<TipoItem> Requisitos => _requisitos.AsReadOnly();

        private RegraDesconto() { }

        public RegraDesconto(string nome, decimal percentual, List<TipoItem> requisitos)
        {
            Nome = nome;
            Percentual = percentual;
            _requisitos = requisitos ?? new();
            Ativo = true;
        }

        public bool SeAplica(IEnumerable<TipoItem> tiposNoPedido)
        {
            if (!Ativo) return false;

            return Requisitos.All(req => tiposNoPedido.Contains(req));
        }
    }
}
