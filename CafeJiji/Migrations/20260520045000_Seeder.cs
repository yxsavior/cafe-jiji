using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeJiji.Migrations
{
    /// <inheritdoc />
    public partial class Seeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(6939), new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(6703) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7106), new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7105) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7107), new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7107) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7109), new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7108) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7110), new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7111), new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7111) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7113), new DateTime(2026, 5, 20, 1, 49, 59, 943, DateTimeKind.Local).AddTicks(7112) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4812), new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4625) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4976), new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4976) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4978), new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4977) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4979), new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4979) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4980), new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4982), new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4982) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4983), new DateTime(2026, 5, 20, 1, 42, 2, 534, DateTimeKind.Local).AddTicks(4983) });
        }
    }
}
