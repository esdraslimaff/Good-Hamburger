using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Shared.DTOs;
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var promos = await _service.ObterAtivasAsync();
            return Ok(promos);
        }
    }
}
