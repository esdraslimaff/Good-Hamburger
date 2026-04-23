using GoodHamburger.Domain.Enums;

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
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome da promoção não pode ser nulo ou vazio.", nameof(nome));
            }

            if (percentual < 0 || percentual > 1m)
            {
                throw new ArgumentException("O percentual de desconto deve estar entre 0 e 1 (0% e 100%).", nameof(percentual));
            }

            Nome = nome;
            Percentual = percentual;
            Ativo = true;
        }

        public bool ContemTodosRequisitos(List<TipoItem> itensPedido) 
        {
            if (itensPedido == null || _requisitos.Count == 0) return false;
            var requisitos = _requisitos.Select(r => r.TipoItem);
            return requisitos.All(r => itensPedido.Contains(r)); 
        }

        public void AdicionarRequisito(TipoItem tipo)
        {
            if (_requisitos.Any(r => r.TipoItem == tipo)) return;
            var requisito = new PromocaoItem(tipo);
            _requisitos.Add(requisito);
        }

        public void AlternarStatus()
        {
            Ativo = !Ativo;
        }

    }
}
