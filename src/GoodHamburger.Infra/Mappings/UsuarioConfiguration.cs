using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infra.Mappings
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.Property(u => u.SenhaHash)
                   .IsRequired();

            builder.Property(u => u.Perfil)
                   .IsRequired()
                   .HasConversion<int>();

            var senhaFixaHash = "$2a$12$MxtoBxZcMUBXeCfpog03a.SbV2eeH/kbRsBGmK8oY.1rIMtgF6Uti"; // TO-DO: Deixar claro no READM que essa senha em hash equivale a '123456'

            builder.HasData(new
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Nome = "Administrador",
                Email = "admin@goodhamburger.com",
                SenhaHash = senhaFixaHash,
                Perfil = TipoPerfil.Admin,
                Ativo = true,
                DataCriacao = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            });
        }
    }
}