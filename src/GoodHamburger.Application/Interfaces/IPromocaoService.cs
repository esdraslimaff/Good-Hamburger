using GoodHamburger.Shared.DTOs;

namespace GoodHamburger.Application.Interfaces
{
    public interface IPromocaoService
    {
        Task<IEnumerable<PromocaoDto>> ObterAtivasAsync();
        Task<IEnumerable<PromocaoDto>> ObterTodasPromocoesAsync();
        Task<PromocaoDto> BuscarPromocaoComRequisitosPorIdAsync(Guid id);
        Task AlternarStatusAsync(Guid id);

    }
}
