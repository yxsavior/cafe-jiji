using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeJiji.Migrations
{
    /// <inheritdoc />
    public partial class CorrigiNomeAdotanteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdontanteId",
                table: "Gatos");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdontanteId",
                table: "Gatos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Gatos",
                keyColumn: "Id",
                keyValue: 1,
                column: "AdontanteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Gatos",
                keyColumn: "Id",
                keyValue: 2,
                column: "AdontanteId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Gatos",
                keyColumn: "Id",
                keyValue: 3,
                column: "AdontanteId",
                value: null);

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
    }
}
