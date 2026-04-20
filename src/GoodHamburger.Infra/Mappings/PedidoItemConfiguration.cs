using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Infra.Mappings
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");
            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(pi => pi.PrecoUnitario)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(pi => pi.Tipo)
                   .IsRequired();

            builder.Property(pi => pi.ProdutoId)
                   .IsRequired();
        }
    }
}
