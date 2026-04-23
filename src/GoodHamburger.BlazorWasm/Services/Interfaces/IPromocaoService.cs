using GoodHamburger.Shared.DTOs;

namespace GoodHamburger.BlazorWasm.Services.Interfaces
{
    public interface IPromocaoService
    {
        Task<List<PromocaoDto>> GetPromocoesAtivasAsync();
        Task<List<PromocaoDto>> GetPromocoesAsync();
        Task<PromocaoDto> GetPromocaoPorIdAsync(Guid id);
        Task AlternarStatusAsync(Guid id);
    }
}
