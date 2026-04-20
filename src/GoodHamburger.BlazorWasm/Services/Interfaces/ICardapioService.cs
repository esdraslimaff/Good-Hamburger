using GoodHamburger.Shared.DTOs;

namespace GoodHamburger.BlazorWasm.Services.Interfaces
{
    public interface ICardapioService
    {
        Task<List<ItemCardapioDto>> GetItensAsync();
    }
}
