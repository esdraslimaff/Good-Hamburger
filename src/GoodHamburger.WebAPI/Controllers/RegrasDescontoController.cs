using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegrasDescontoController : ControllerBase
    {
        private readonly IRegraDescontoRepository _repository;

        public RegrasDescontoController(IRegraDescontoRepository repository)
            => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var regras = await _repository.ObterTodasAtivasAsync();

            var dtos = regras.Select(r => new RegraDescontoDto
            {
                Nome = r.Nome,
                Percentual = r.Percentual,
                Requisitos = r.Requisitos
                .Select(x => x.TipoItem)
                .ToList()
            });

            return Ok(dtos);
        }
    }
}
