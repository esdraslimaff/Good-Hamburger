using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository:IBaseRepository<Usuario>
    {
        Task<Usuario?> ObterPorEmailAsync(string email);
    }
}
