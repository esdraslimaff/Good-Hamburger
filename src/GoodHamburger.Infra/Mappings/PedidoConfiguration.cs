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

            var navigation = builder.Metadata.FindNavigation(nameof(Pedido.Itens));
            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(p => p.Itens)
                   .WithMany()
                   .UsingEntity(j => j.ToTable("PedidoItens"));
        }
    }
}
