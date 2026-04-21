using GoodHamburger.Application.Interfaces;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<ActionResult<PedidoResponse>> Post([FromBody] PedidoRequest request)
        {
            var response = await _pedidoAppService.CriarPedidoAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PedidoResponse>> GetById(Guid id)
        {
            var pedido = await _pedidoAppService.ObterPorIdAsync(id);
            if (pedido == null) return NotFound();

            return Ok(pedido);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoResponse>>> Get()
        {
            var pedidos = await _pedidoAppService.ListarTodosAsync();
            return Ok(pedidos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pedidoAppService.RemoverAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PedidoRequest request)
        {
            await _pedidoAppService.AtualizarPedidoAsync(id, request);
            return NoContent();
        }
    }
}
