using GoodHamburger.Application.Interfaces;
using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Services
{
    public class PromocaoService : IPromocaoService
    {
        private readonly IPromocaoRepository _repository;

        public PromocaoService(IPromocaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PromocaoDto>> ObterAtivasAsync()
        {
            var regras = await _repository.ObterTodasAtivasAsync();

            return regras.Select(r => new PromocaoDto
            {
                Nome = r.Nome,
                Percentual = r.Percentual,
                Requisitos = r.Requisitos
                    .Select(x => x.TipoItem)
                    .ToList()
            });
        }
    }
}
