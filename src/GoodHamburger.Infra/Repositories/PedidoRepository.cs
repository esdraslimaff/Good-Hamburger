using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Infra.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(AppDbContext context) : base(context) { }

        public async Task<Pedido?> GetPedidoComItensAsync(Guid id)
        {
            return await DbSet
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await DbSet
                .Include(p => p.Itens)
                .OrderByDescending(p => p.DataCriacao)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
