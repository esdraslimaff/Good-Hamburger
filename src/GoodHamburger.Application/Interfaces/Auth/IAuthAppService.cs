using GoodHamburger.Shared.DTOs;

namespace GoodHamburger.Application.Interfaces.Auth
{
    public interface IAuthAppService
    {
        Task<string> LoginAsync(LoginRequestDto request);
    }
}
