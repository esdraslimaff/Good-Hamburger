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
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DataCriacao).IsRequired();
            builder.Property(p => p.DescontoPercentual).HasPrecision(5, 2);
            builder.Property(p => p.Subtotal).HasPrecision(18, 2);
            builder.Property(p => p.ValorDesconto).HasPrecision(18, 2);
            builder.Property(p => p.TotalFinal).HasPrecision(18, 2);

            builder.HasMany(p => p.Itens)
                   .WithOne()
                   .HasForeignKey("PedidoId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
