using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoodHamburger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class MigrationSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DescontoPercentual = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalFinal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PromocaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promocao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Percentual = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoItens_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromocaoItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromocaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoItem = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocaoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromocaoItens_Promocao_PromocaoId",
                        column: x => x.PromocaoId,
                        principalTable: "Promocao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Itens",
                columns: new[] { "Id", "DataCriacao", "DataUltimaAlteracao", "Nome", "PrecoUnitario", "Tipo" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4762), null, "X Burger", 5.00m, 1 },
                    { new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"), new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4769), null, "X Egg", 4.50m, 1 },
                    { new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"), new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4773), null, "X Bacon", 7.00m, 1 },
                    { new Guid("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a"), new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4775), null, "Batata frita", 2.00m, 2 },
                    { new Guid("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b"), new DateTime(2026, 4, 24, 3, 17, 27, 2, DateTimeKind.Utc).AddTicks(4777), null, "Refrigerante", 2.50m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Promocao",
                columns: new[] { "Id", "Ativo", "DataCriacao", "DataUltimaAlteracao", "Nome", "Percentual" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"), true, new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(5), null, "Combo Completo", 0.20m },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"), true, new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(11), null, "Lanche e Refri", 0.15m },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), true, new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(14), null, "Lanche e Batata", 0.10m }
                });

            migrationBuilder.InsertData(
                table: "PromocaoItens",
                columns: new[] { "Id", "DataCriacao", "DataUltimaAlteracao", "PromocaoId", "TipoItem" },
                values: new object[,]
                {
                    { new Guid("a4d5e6f7-9b0c-4d1e-8f2a-2b3c4d5e6006"), new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3936), null, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), 1 },
                    { new Guid("a7d3c1f0-2e4b-4d9a-8c1f-5e7b2a6c9001"), new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3918), null, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"), 1 },
                    { new Guid("b5e6f7a8-0c1d-4e2f-9a3b-3c4d5e6f7007"), new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3939), null, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d473"), 2 },
                    { new Guid("b8e4d2a1-3c5f-4a8b-9d2e-6f1a3c7b9002"), new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3925), null, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"), 3 },
                    { new Guid("c9f5e3b2-4d6a-4b7c-8e3f-7a2b4d8c9003"), new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3929), null, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d471"), 2 },
                    { new Guid("e1b2c3d4-6f7a-4b8c-9d0e-8a1b2c3d4004"), new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3932), null, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"), 1 },
                    { new Guid("f2c3d4e5-7a8b-4c9d-0e1f-9b2c3d4e5005"), new DateTime(2026, 4, 24, 3, 17, 27, 4, DateTimeKind.Utc).AddTicks(3934), null, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d472"), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidoId",
                table: "PedidoItens",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PromocaoItens_PromocaoId",
                table: "PromocaoItens",
                column: "PromocaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "PedidoItens");

            migrationBuilder.DropTable(
                name: "PromocaoItens");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Promocao");
        }
    }
}
