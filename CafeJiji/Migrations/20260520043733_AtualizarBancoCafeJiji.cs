using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeJiji.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarBancoCafeJiji : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3667), new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3311) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3885), new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3885) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3887), new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3887) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3923), new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3923) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3925), new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3925) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3926), new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3926) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3928), new DateTime(2026, 5, 20, 1, 37, 33, 413, DateTimeKind.Local).AddTicks(3927) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2486), new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2311) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2644), new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2644) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2646), new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2645) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2647), new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2647) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2648), new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2648) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2650), new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AtualizadoEm", "CriadoEm" },
                values: new object[] { new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2651), new DateTime(2026, 5, 20, 1, 34, 6, 663, DateTimeKind.Local).AddTicks(2651) });
        }
    }
}
