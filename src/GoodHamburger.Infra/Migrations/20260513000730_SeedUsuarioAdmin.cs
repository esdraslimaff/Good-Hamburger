using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodHamburger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Perfil = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 18, DateTimeKind.Utc).AddTicks(1742));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 18, DateTimeKind.Utc).AddTicks(1745));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 18, DateTimeKind.Utc).AddTicks(1746));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 18, DateTimeKind.Utc).AddTicks(1747));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 18, DateTimeKind.Utc).AddTicks(1748));

            migrationBuilder.UpdateData(
                table: "Promocao",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 18, DateTimeKind.Utc).AddTicks(9291));

            migrationBuilder.UpdateData(
                table: "Promocao",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 18, DateTimeKind.Utc).AddTicks(9296));

            migrationBuilder.UpdateData(
                table: "Promocao",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 18, DateTimeKind.Utc).AddTicks(9298));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("a4d5e6f7-9b0c-4d1e-8f2a-2b3c4d5e6006"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 19, DateTimeKind.Utc).AddTicks(1157));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("a7d3c1f0-2e4b-4d9a-8c1f-5e7b2a6c9001"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 19, DateTimeKind.Utc).AddTicks(1145));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("b5e6f7a8-0c1d-4e2f-9a3b-3c4d5e6f7007"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 19, DateTimeKind.Utc).AddTicks(1158));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("b8e4d2a1-3c5f-4a8b-9d2e-6f1a3c7b9002"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 19, DateTimeKind.Utc).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("c9f5e3b2-4d6a-4b7c-8e3f-7a2b4d8c9003"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 19, DateTimeKind.Utc).AddTicks(1151));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("e1b2c3d4-6f7a-4b8c-9d0e-8a1b2c3d4004"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 19, DateTimeKind.Utc).AddTicks(1154));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("f2c3d4e5-7a8b-4c9d-0e1f-9b2c3d4e5005"),
                column: "DataCriacao",
                value: new DateTime(2026, 5, 13, 0, 7, 30, 19, DateTimeKind.Utc).AddTicks(1155));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Ativo", "DataCriacao", "DataUltimaAlteracao", "Email", "Nome", "Perfil", "SenhaHash" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "admin@goodhamburger.com", "Administrador", 1, "$2a$11$xO.8W7K8G.kY6I6aD5g/2.QdY3k1s1uU0xXw0/A5b2O1e/wP7e5sC" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4762));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4769));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4773));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4775));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: new Guid("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4777));

            migrationBuilder.UpdateData(
                table: "Promocao",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(5));

            migrationBuilder.UpdateData(
                table: "Promocao",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(11));

            migrationBuilder.UpdateData(
                table: "Promocao",
                keyColumn: "Id",
                keyValue: new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(14));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("a4d5e6f7-9b0c-4d1e-8f2a-2b3c4d5e6006"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3936));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("a7d3c1f0-2e4b-4d9a-8c1f-5e7b2a6c9001"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3918));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("b5e6f7a8-0c1d-4e2f-9a3b-3c4d5e6f7007"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3939));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("b8e4d2a1-3c5f-4a8b-9d2e-6f1a3c7b9002"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3925));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("c9f5e3b2-4d6a-4b7c-8e3f-7a2b4d8c9003"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3929));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("e1b2c3d4-6f7a-4b8c-9d0e-8a1b2c3d4004"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3932));

            migrationBuilder.UpdateData(
                table: "PromocaoItens",
                keyColumn: "Id",
                keyValue: new Guid("f2c3d4e5-7a8b-4c9d-0e1f-9b2c3d4e5005"),
                column: "DataCriacao",
                value: new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3934));
        }
    }
}
