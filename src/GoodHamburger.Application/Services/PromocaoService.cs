using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Shared.DTOs;
using Mapster;

namespace GoodHamburger.Application.Services
{
    public class PromocaoService : IPromocaoService
    {
        private readonly IPromocaoRepository _repository;

        public PromocaoService(IPromocaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PromocaoDto>> ObterAtivasAsync()
        {
            var regras = await _repository.ObterTodasAtivasAsync();

            return regras.Select(r => new PromocaoDto
            {   Id = r.Id,
                Nome = r.Nome,
                Percentual = r.Percentual,
                Ativo = r.Ativo,
                Requisitos = r.Requisitos
                    .Select(x => x.TipoItem)
                    .ToList()
            });
        }

        public async Task<IEnumerable<PromocaoDto>> ObterTodasPromocoesAsync()
        {
            var regras = await _repository.ObterTodasPromocoesComRequisitosAsync();

            return regras.Select(r => new PromocaoDto
            {
                Id = r.Id,
                Nome = r.Nome,
                Percentual = r.Percentual,
                Ativo = r.Ativo,
                Requisitos = r.Requisitos != null
                    ? r.Requisitos.Select(x => x.TipoItem).ToList()
                    : new List<TipoItem>()
            });
        }

        public async Task<PromocaoDto> BuscarPromocaoComRequisitosPorIdAsync(Guid id)
        {
            var promocao = await _repository.BuscarPromocaoComRequisitosPorIdAsync(id);

            if (promocao == null)
                return null;

            return new PromocaoDto
            {
                Id = promocao.Id,
                Nome = promocao.Nome,
                Percentual = promocao.Percentual,
                Ativo = promocao.Ativo,
                Requisitos = promocao.Requisitos
                    .Select(r => r.TipoItem)
                    .ToList()
            };
        }

        public async Task AlternarStatusAsync(Guid id)
        {
            var promocao = await _repository.GetByIdAsync(id);

            if (promocao == null) throw new DomainException("Promoção não encontrada.");

            promocao.AlternarStatus();

            await _repository.UpdateAsync(promocao);
        }
    }
}
