using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Shared.DTOs;

namespace GoodHamburger.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioAppService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioResponseDto> CriarUsuarioAsync(UsuarioRequestDto request)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(request.Email);
            if (usuarioExistente != null)
                throw new DomainException("Já existe um usuário cadastrado com este e-mail.");

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);
            var perfil = (TipoPerfil)request.Perfil;

            var novoUsuario = new Usuario(request.Nome, request.Email, senhaHash, perfil);

            await _usuarioRepository.AddAsync(novoUsuario);

            return AdaptarParaDto(novoUsuario);
        }

        public async Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(AdaptarParaDto);
        }

        public async Task<UsuarioResponseDto?> ObterPorIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario != null ? AdaptarParaDto(usuario) : null;
        }

        public async Task AlternarStatusAsync(Guid id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) throw new DomainException("Usuário não encontrado.");

            if (usuario.Ativo)
                usuario.Inativar();
            else
                usuario.Ativar();

            await _usuarioRepository.UpdateAsync(usuario);
        }
        private UsuarioResponseDto AdaptarParaDto(Usuario usuario)
        {
            return new UsuarioResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil.ToString(),
                Ativo = usuario.Ativo,
                DataCriacao = usuario.DataCriacao
            };
        }
    }
}
