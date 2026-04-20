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
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Percentual)
                .HasPrecision(5, 2);

            builder.Property(r => r.Ativo)
                .IsRequired();


            //TO-DO: ALTERAR ESSA CONVERSÃO, PARA CRIAR ALGO COMO UM TIPO ISOLADO PARA PROMOCOES, E NÃO SALVAR TODOS EM UMA STRING SÓ EX: "1,3,2"
            builder.Property(r => r.Requisitos)
                .HasConversion(
                    v => string.Join(',', v.Select(x => (int)x)),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(x => (TipoItem)int.Parse(x)).ToList() 
                );

            builder.HasData(
                new
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d471"),
                    Nome = "Combo Completo",
                    Percentual = 0.20m,
                    Requisitos = new List<TipoItem> { TipoItem.Sanduiche, TipoItem.Bebida, TipoItem.Acompanhamento },
                    Ativo = true, // 👈 obrigatório agora
                    DataCriacao = DateTime.UtcNow
                },
                new
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d472"),
                    Nome = "Lanche e Refri",
                    Percentual = 0.15m,
                    Requisitos = new List<TipoItem> { TipoItem.Sanduiche, TipoItem.Bebida },
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                },
                new
                {
                    Id = Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                    Nome = "Lanche e Batata",
                    Percentual = 0.10m,
                    Requisitos = new List<TipoItem> { TipoItem.Sanduiche, TipoItem.Acompanhamento },
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                }
            );
        }
    }
}
