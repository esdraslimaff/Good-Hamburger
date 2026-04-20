using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Infra.Mappings
{
    public class RegraDescontoItemConfiguration : IEntityTypeConfiguration<RegraDescontoItem>
    {
        public void Configure(EntityTypeBuilder<RegraDescontoItem> builder)
        {
            builder.ToTable("RegraDescontoItens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TipoItem)
                .HasConversion<int>() 
                .IsRequired();

            builder.Property<Guid>("RegraDescontoId");

            builder.HasData(
                            new
                            {
                                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                                RegraDescontoId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d471"),
                                TipoItem = TipoItem.Sanduiche,
                                DataCriacao = DateTime.UtcNow
                            },
                            new
                            {
                                Id = Guid.Parse("11111111-1111-1111-1111-111111111112"),
                                RegraDescontoId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d471"),
                                TipoItem = TipoItem.Bebida,
                                DataCriacao = DateTime.UtcNow
                            },
                            new
                            {
                                Id = Guid.Parse("11111111-1111-1111-1111-111111111113"),
                                RegraDescontoId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d471"),
                                TipoItem = TipoItem.Acompanhamento,
                                DataCriacao = DateTime.UtcNow
                            },

                            new
                            {
                                Id = Guid.Parse("11111111-1111-1111-1111-111111111114"),
                                RegraDescontoId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d472"),
                                TipoItem = TipoItem.Sanduiche,
                                DataCriacao = DateTime.UtcNow
                            },
                            new
                            {
                                Id = Guid.Parse("11111111-1111-1111-1111-111111111115"),
                                RegraDescontoId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d472"),
                                TipoItem = TipoItem.Bebida,
                                DataCriacao = DateTime.UtcNow
                            },

                            new
                            {
                                Id = Guid.Parse("11111111-1111-1111-1111-111111111116"),
                                RegraDescontoId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                                TipoItem =  TipoItem.Sanduiche,
                                DataCriacao = DateTime.UtcNow
                            },
                            new
                            {
                                Id = Guid.Parse("11111111-1111-1111-1111-111111111117"),
                                RegraDescontoId = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                                TipoItem = TipoItem.Acompanhamento,
                                DataCriacao = DateTime.UtcNow
                            }
                        );
        }
    }
}
