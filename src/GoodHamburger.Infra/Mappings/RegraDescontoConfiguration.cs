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
    public class RegraDescontoConfiguration : IEntityTypeConfiguration<RegraDesconto>
    {
        public void Configure(EntityTypeBuilder<RegraDesconto> builder)
        {
            builder.ToTable("RegrasDesconto");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Percentual)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(r => r.Ativo)
                .IsRequired();

            builder.Property(r => r.DataCriacao)
                .IsRequired();

            builder.HasMany(r => r.Requisitos)
                .WithOne()
                .HasForeignKey("RegraDescontoId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
            new
            {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d471"),
                Nome = "Combo Completo",
                Percentual = 0.20m,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            },
            new
            {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d472"),
                Nome = "Lanche e Refri",
                Percentual = 0.15m,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            },
            new
            {
                Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                Nome = "Lanche e Batata",
                Percentual = 0.10m,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            }
        );
        }
    }
}
