using GoodHamburger.Application.Interfaces.Auth;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebAPI.Controllers
{
    /// <summary>
    /// Responsável pela autenticação de usuários no sistema.
    /// </summary>
    /// <remarks>
    /// Permite que um usuário informe suas credenciais (e-mail e senha)
    /// e receba um token JWT para acessar endpoints protegidos.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authAppService;

        /// <summary>
        /// Inicializa uma nova instância do controller de autenticação.
        /// </summary>
        /// <param name="authAppService">
        /// Serviço de aplicação responsável por validar as credenciais
        /// e gerar o token JWT.
        /// </param>
        public AuthController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT.
        /// </summary>
        /// <param name="request">
        /// Dados de autenticação contendo e-mail e senha.
        /// </param>
        /// <returns>
        /// Um objeto contendo o token JWT que deverá ser enviado
        /// no cabeçalho Authorization das próximas requisições.
        /// </returns>
        /// <response code="200">
        /// Login realizado com sucesso. Retorna o token JWT.
        /// </response>
        /// <response code="400">
        /// Usuário ou senha inválidos.
        /// </response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var token = await _authAppService.LoginAsync(request);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}