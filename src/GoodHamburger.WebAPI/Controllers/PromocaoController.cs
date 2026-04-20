using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromocaoController : ControllerBase
    {
        private readonly IPromocaoRepository _repository;

        public PromocaoController(IPromocaoRepository repository)
            => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var regras = await _repository.ObterTodasAtivasAsync();

            var dtos = regras.Select(r => new PromocaoDto
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
