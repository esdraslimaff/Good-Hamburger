using GoodHamburger.Application.Interfaces.Auth;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public AuthAppService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(LoginRequestDto request)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            if (usuario == null || !usuario.Ativo)
                throw new DomainException("Usuário ou senha inválidos.");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash);

            if (!senhaValida)
                throw new DomainException("Usuário ou senha inválidos.");

            var token = _tokenService.GenerateToken(usuario.Email, usuario.Perfil.ToString());

            return token;
        }
    }
}
