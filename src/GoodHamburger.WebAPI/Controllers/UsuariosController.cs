using GoodHamburger.Application.Interfaces;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebAPI.Controllers
{
    /// <summary>
    /// Responsável pelo gerenciamento de usuários do sistema.
    /// </summary>
    /// <remarks>
    /// Este controller permite:
    /// - Criar novos usuários;
    /// - Listar usuários cadastrados;
    /// - Consultar um usuário por ID;
    /// - Ativar ou desativar usuários.
    ///
    /// O acesso é restrito a usuários com perfil Admin.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAppService _appService;

        /// <summary>
        /// Inicializa uma nova instância do controller de usuários.
        /// </summary>
        /// <param name="appService">
        /// Serviço de aplicação responsável pelas operações de usuários.
        /// </param>
        public UsuariosController(IUsuarioAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <param name="request">
        /// Dados do usuário, incluindo nome, e-mail, senha e perfil.
        /// </param>
        /// <returns>
        /// O usuário criado com sucesso.
        /// </returns>
        /// <response code="201">
        /// Usuário criado com sucesso.
        /// </response>
        /// <response code="400">
        /// Dados inválidos ou e-mail já cadastrado.
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UsuarioRequestDto request)
        {
            try
            {
                var response = await _appService.CriarUsuarioAsync(request);

                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns>
        /// Lista de usuários.
        /// </returns>
        /// <response code="200">
        /// Lista retornada com sucesso.
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _appService.ObterTodosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Consulta um usuário específico pelo seu identificador.
        /// </summary>
        /// <param name="id">
        /// Identificador único (GUID) do usuário.
        /// </param>
        /// <returns>
        /// Dados do usuário encontrado.
        /// </returns>
        /// <response code="200">
        /// Usuário encontrado.
        /// </response>
        /// <response code="404">
        /// Usuário não encontrado.
        /// </response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var usuario = await _appService.ObterPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Alterna o status do usuário entre ativo e inativo.
        /// </summary>
        /// <param name="id">
        /// Identificador único (GUID) do usuário.
        /// </param>
        /// <response code="204">
        /// Status alterado com sucesso.
        /// </response>
        /// <response code="400">
        /// Erro ao alterar o status.
        /// </response>
        [HttpPatch("{id:guid}/alternar-status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AlternarStatus(Guid id)
        {
            try
            {
                await _appService.AlternarStatusAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}