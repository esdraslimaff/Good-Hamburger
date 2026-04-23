using GoodHamburger.Application.Interfaces;
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

        [HttpGet("PromocoesAtivas")]
        public async Task<IActionResult> GetAtivasAsync()
        {
            var promos = await _service.ObterAtivasAsync();
            return Ok(promos);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var promos = await _service.ObterTodasPromocoesAsync();
            return Ok(promos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var promocao = await _service.BuscarPromocaoComRequisitosPorIdAsync(id);

            if (promocao == null)
                return NotFound();

            return Ok(promocao);
        }

        [HttpPatch("{id}/alternar-status")]
        public async Task<IActionResult> AlternarStatus(Guid id)
        {
            await _service.AlternarStatusAsync(id);
            return NoContent();
        }
    }
}
