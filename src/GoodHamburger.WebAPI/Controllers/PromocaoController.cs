using GoodHamburger.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromocaoController : ControllerBase
    {
        private readonly IPromocaoService _service;

        public PromocaoController(IPromocaoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém a lista de promoções que estão atualmente ativas no sistema.
        /// </summary>
        /// <returns>Uma lista contendo apenas as promoções vigentes.</returns>
        /// <response code="200">Retorna a lista de promoções ativas.</response>
        [HttpGet("PromocoesAtivas")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAtivasAsync()
        {
            var promos = await _service.ObterAtivasAsync();
            return Ok(promos);
        }

        /// <summary>
        /// Obtém a lista completa de todas as promoções (ativas e inativas).
        /// </summary>
        /// <returns>Uma lista com o histórico completo de promoções.</returns>
        /// <response code="200">Retorna todas as promoções cadastradas.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var promos = await _service.ObterTodasPromocoesAsync();
            return Ok(promos);
        }

        /// <summary>
        /// Consulta os detalhes de uma promoção específica.
        /// </summary>
        /// <param name="id">O identificador único (GUID) da promoção.</param>
        /// <returns>A promoção encontrada com seus respectivos requisitos.</returns>
        /// <response code="200">Retorna a promoção encontrada.</response>
        /// <response code="404">Se a promoção não for encontrada na base de dados.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var promocao = await _service.BuscarPromocaoComRequisitosPorIdAsync(id);

            if (promocao == null)
                return NotFound();

            return Ok(promocao);
        }

        /// <summary>
        /// Alterna o status de uma promoção (ativa para inativa, ou vice-versa).
        /// </summary>
        /// <param name="id">O identificador único (GUID) da promoção.</param>
        /// <response code="204">Status da promoção alterado com sucesso.</response>
        /// <response code="404">Se a promoção não for encontrada.</response>
        [HttpPatch("{id}/alternar-status")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AlternarStatus(Guid id)
        {
            await _service.AlternarStatusAsync(id);
            return NoContent();
        }
    }
}