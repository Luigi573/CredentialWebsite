using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class StarterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID_Centro",
                table: "Alumnos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Centros",
                columns: new[] { "ID_Centro", "clave", "nombre" },
                values: new object[,]
                {
                    { 1, "30ETH0224D", "Telebachillerato Coacotla" },
                    { 2, "30ETH0206O", "Telebachillerato Nopalapan" },
                    { 3, "30ETH0472L", "Telebachillerato Lealtad de Muñoz" },
                    { 4, "30ETH0134L", "Telebachillerato Isla" }
                });

            migrationBuilder.InsertData(
                table: "CiclosEscolares",
                columns: new[] { "ID_PeriodoEscolar", "fechaFin", "activo", "nombre", "fechaInicio" },
                values: new object[] { 1, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "2024-2025", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_ID_Centro",
                table: "Alumnos",
                column: "ID_Centro");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_Centros_ID_Centro",
                table: "Alumnos",
                column: "ID_Centro",
                principalTable: "Centros",
                principalColumn: "ID_Centro",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_Centros_ID_Centro",
                table: "Alumnos");

            migrationBuilder.DropIndex(
                name: "IX_Alumnos_ID_Centro",
                table: "Alumnos");

            migrationBuilder.DeleteData(
                table: "Centros",
                keyColumn: "ID_Centro",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Centros",
                keyColumn: "ID_Centro",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Centros",
                keyColumn: "ID_Centro",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Centros",
                keyColumn: "ID_Centro",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CiclosEscolares",
                keyColumn: "ID_PeriodoEscolar",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "ID_Centro",
                table: "Alumnos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
