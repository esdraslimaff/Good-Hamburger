using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces.Repository;
using GoodHamburger.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Infra.Repositories
{
    public class PromocaoRepository : IPromocaoRepository
    {
        private readonly AppDbContext _context;

        public PromocaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promocao>> ObterTodasAtivasAsync()
        {
            return await _context.Promocao
                        .Include(r=>r.Requisitos)
                        .Where(x => x.Ativo)
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}
