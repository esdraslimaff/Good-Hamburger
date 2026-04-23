using GoodHamburger.Application.Interfaces;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoAppService _pedidoAppService;

        public PedidosController(IPedidoAppService pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }

        /// <summary>
        /// Cria um novo pedido aplicando as regras de promoção vigentes.
        /// </summary>
        /// <param name="request">Lista de IDs dos itens desejados (máximo 3 itens: 1 sanduíche, 1 acompanhamento, 1 bebida).</param>
        /// <returns>O pedido recém-criado com os totais calculados.</returns>
        /// <response code="201">Pedido criado com sucesso.</response>
        /// <response code="400">Se a validação falhar (ex: itens repetidos ou limite excedido).</response>
        [HttpPost]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoResponse>> Post([FromBody] PedidoRequest request)
        {
            var response = await _pedidoAppService.CriarPedidoAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Consulta um pedido específico pelo seu identificador (ID).
        /// </summary>
        /// <param name="id">O identificador único (GUID) do pedido.</param>
        /// <returns>Os detalhes do pedido solicitado.</returns>
        /// <response code="200">Retorna o pedido encontrado.</response>
        /// <response code="404">Se o pedido não for encontrado na base de dados.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PedidoResponse>> GetById(Guid id)
        {
            var pedido = await _pedidoAppService.ObterPorIdAsync(id);
            if (pedido == null) return NotFound();

            return Ok(pedido);
        }

        /// <summary>
        /// Lista todos os pedidos registrados no sistema.
        /// </summary>
        /// <returns>Uma lista de pedidos.</returns>
        /// <response code="200">Retorna a lista de pedidos com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PedidoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PedidoResponse>>> Get()
        {
            var pedidos = await _pedidoAppService.ListarTodosAsync();
            return Ok(pedidos);
        }

        /// <summary>
        /// Remove um pedido do sistema.
        /// </summary>
        /// <param name="id">O identificador único (GUID) do pedido a ser removido.</param>
        /// <response code="204">Pedido removido com sucesso (sem conteúdo de retorno).</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pedidoAppService.RemoverAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Atualiza os itens de um pedido existente, recalculando os totais e promoções.
        /// </summary>
        /// <param name="id">O identificador único (GUID) do pedido.</param>
        /// <param name="request">A nova lista de IDs dos itens do pedido.</param>
        /// <response code="204">Pedido atualizado com sucesso.</response>
        /// <response code="400">Se houver erro de validação nas regras de negócio.</response>
        /// <response code="404">Se o pedido não for encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] PedidoRequest request)
        {
            await _pedidoAppService.AtualizarPedidoAsync(id, request);
            return NoContent();
        }
    }
}