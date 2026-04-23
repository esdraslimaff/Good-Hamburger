using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Shared.DTOs
{
    public class PromocaoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Percentual { get; set; }
        public bool Ativo { get; set; }
        public List<TipoItem> Requisitos { get; set; } = new();

        public PromocaoDto()
        {
            
        }

    }
}
