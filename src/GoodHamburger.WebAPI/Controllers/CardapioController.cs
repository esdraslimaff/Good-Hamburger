using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Shared.DTOs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardapioController : ControllerBase
    {
        private readonly ICardapioService _service;

        public CardapioController(ICardapioService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém a lista completa de itens do cardápio disponíveis na lanchonete.
        /// </summary>
        /// <returns>Uma lista de itens do cardápio com seus respectivos preços e tipos.</returns>
        /// <response code="200">Retorna a lista de itens do cardápio com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemCardapioDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var itens = await _service.ObterItensAsync();
            return Ok(itens);
        }
    }
}
