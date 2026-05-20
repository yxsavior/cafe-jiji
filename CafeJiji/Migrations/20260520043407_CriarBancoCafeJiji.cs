using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeJiji.Migrations
{
    /// <inheritdoc />
    public partial class CriarBancoCafeJiji : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Adotantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CPF = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adotantes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Insumos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantidadeAtual = table.Column<int>(type: "int", nullable: false),
                    EstoqueMinimo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preco = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Categoria = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantidadeEstoque = table.Column<int>(type: "int", nullable: false),
                    EstoqueMinimo = table.Column<int>(type: "int", nullable: false),
                    RequerPreparo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SenhaHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Perfil = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Gatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataChegada = table.Column<DateOnly>(type: "date", nullable: false),
                    DataAdotacao = table.Column<DateOnly>(type: "date", nullable: true),
                    FotoUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroProtocoloONG = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdontanteId = table.Column<int>(type: "int", nullable: true),
                    AdotanteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gatos_Adotantes_AdotanteId",
                        column: x => x.AdotanteId,
                        principalTable: "Adotantes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroMesa = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItensPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Gatos",
                columns: new[] { "Id", "AdontanteId", "AdotanteId", "DataAdotacao", "DataChegada", "FotoUrl", "Nome", "NumeroProtocoloONG", "Status" },
                values: new object[,]
                {
                    { 1, null, null, null, new DateOnly(2026, 4, 10), "https://images.unsplash.com/photo-1514888286974-6c03e2ca1dba?q=80&w=500", "Mingau", null, 0 },
                    { 2, null, null, null, new DateOnly(2026, 5, 1), "https://images.unsplash.com/photo-1573865526739-10659fec78a5?q=80&w=500", "Frajola", null, 0 },
                    { 3, null, null, null, new DateOnly(2026, 3, 15), "https://images.unsplash.com/photo-1533738363-b7f9aef128ce?q=80&w=500", "Paçoca", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Insumos",
                columns: new[] { "Id", "EstoqueMinimo", "Nome", "QuantidadeAtual" },
                values: new object[,]
                {
                    { 1, 2, "Fardo Leite Integral (12L)", 5 },
                    { 2, 3, "Café em Grãos Blend Especial (1kg)", 8 }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "Categoria", "CriadoEm", "EstoqueMinimo", "Nome", "Preco", "QuantidadeEstoque", "RequerPreparo" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2486), "Cafés", new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2311), 0, "Espresso Tradicional", 7.50m, 999, true },
                    { 2, true, new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2644), "Cafés", new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2644), 0, "Capuccino Gateiro", 12.00m, 999, true },
                    { 3, true, new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2646), "Cafés", new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2645), 0, "Latte Macchiato", 14.50m, 999, true },
                    { 4, true, new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2647), "Doces", new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2647), 3, "Fatia de Torta Holandesa", 16.00m, 12, false },
                    { 5, true, new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2648), "Doces", new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2648), 5, "Brownie de Chocolate", 9.50m, 20, false },
                    { 6, true, new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2650), "Salgados", new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2650), 4, "Pão de Queijo Recheado", 8.00m, 15, false },
                    { 7, true, new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2651), "Serviços", new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2651), 0, "Taxa de Entrada Gatil", 15.00m, 9999, false }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Nome", "Perfil", "SenhaHash", "Username" },
                values: new object[,]
                {
                    { 1, "Kiki", "Gerente", "hash_senior_123", "jiji.senior" },
                    { 2, "Ursula", "Atendente", "hash_junior_123", "jiji.junior" },
                    { 3, "Osono", "Barista", "hash_pleno_123", "jiji.pleno" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gatos_AdotanteId",
                table: "Gatos",
                column: "AdotanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedidos_PedidoId",
                table: "ItensPedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gatos");

            migrationBuilder.DropTable(
                name: "Insumos");

            migrationBuilder.DropTable(
                name: "ItensPedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Adotantes");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
