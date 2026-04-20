using GoodHamburger.Shared.DTOs;

namespace GoodHamburger.BlazorWasm.Services.Interfaces
{
    public interface IPromocaoService
    {
        Task<List<PromocaoDto>> GetPromocoesAsync();
    }
}
