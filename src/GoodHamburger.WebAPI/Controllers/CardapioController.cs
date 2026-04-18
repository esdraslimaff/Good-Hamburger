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
        private readonly IItemRepository _itemRepository;

        public CardapioController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCardapioDto>>> Get()
        {
            var itens = await _itemRepository.GetAllAsync();
            return Ok(itens.Adapt<IEnumerable<ItemCardapioDto>>());
        }
    }
}
