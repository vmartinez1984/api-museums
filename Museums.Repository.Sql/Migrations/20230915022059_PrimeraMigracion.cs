using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museums.Repository.Sql.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crontab",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: true),
                    Hour = table.Column<int>(type: "int", nullable: true),
                    DayOfMonth = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    DayOfWeek = table.Column<int>(type: "int", nullable: true),
                    IsActivate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crontab", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateExecution = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfUpdates = table.Column<int>(type: "int", nullable: false),
                    NumberErrors = table.Column<int>(type: "int", nullable: false),
                    MuseumIdInProcess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalRecords = table.Column<int>(type: "int", nullable: false),
                    DateCancelation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEndExecution = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Museo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MuseoId = table.Column<int>(type: "int", nullable: false),
                    MuseoTematicaN1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuseoNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuseoFechaFundacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuseoAdscripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuseoTipoDePropiedad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuseoCalleNumero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuseoColonia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuseoCp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuseoTelefono1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaginaWeb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaginaWeb2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GmapsLatitud = table.Column<double>(type: "float", nullable: false),
                    GmapsLongitud = table.Column<double>(type: "float", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    LocalidadId = table.Column<int>(type: "int", nullable: false),
                    NomEnt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomMun = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomLoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkSic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoariosYCostos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatosGenerales = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaDeActualizacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Museo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crontab");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Museo");
        }
    }
}
