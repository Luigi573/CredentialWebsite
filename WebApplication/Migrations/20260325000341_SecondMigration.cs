using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "periodoEscolar",
                table: "Alumnos");

            migrationBuilder.AddColumn<int>(
                name: "ID_PeriodoEscolar",
                table: "Alumnos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolYearId",
                table: "Alumnos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CiclosEscolares",
                columns: table => new
                {
                    ID_PeriodoEscolar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false),
                    fechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiclosEscolares", x => x.ID_PeriodoEscolar);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_SchoolYearId",
                table: "Alumnos",
                column: "SchoolYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_CiclosEscolares_SchoolYearId",
                table: "Alumnos",
                column: "SchoolYearId",
                principalTable: "CiclosEscolares",
                principalColumn: "ID_PeriodoEscolar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_CiclosEscolares_SchoolYearId",
                table: "Alumnos");

            migrationBuilder.DropTable(
                name: "CiclosEscolares");

            migrationBuilder.DropIndex(
                name: "IX_Alumnos_SchoolYearId",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "ID_PeriodoEscolar",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "SchoolYearId",
                table: "Alumnos");

            migrationBuilder.AddColumn<string>(
                name: "periodoEscolar",
                table: "Alumnos",
                type: "longtext",
                nullable: false);
        }
    }
}
