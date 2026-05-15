using GoodHamburger.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<UsuarioResponseDto> CriarUsuarioAsync(UsuarioRequestDto request);
        Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync();
        Task<UsuarioResponseDto?> ObterPorIdAsync(Guid id);
        Task AlternarStatusAsync(Guid id);
    }
}
