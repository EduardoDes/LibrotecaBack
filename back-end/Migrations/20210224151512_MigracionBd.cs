using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace back_end.Migrations
{
    public partial class MigracionBd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FechaLectura",
                columns: table => new
                {
                    IdFechaLectura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    FechaDeLectura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadLeidas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechaLectura", x => x.IdFechaLectura);
                });

            migrationBuilder.CreateTable(
                name: "Intereses",
                columns: table => new
                {
                    IdInteres = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intereses", x => x.IdInteres);
                });

            migrationBuilder.CreateTable(
                name: "InteresUsuario",
                columns: table => new
                {
                    IdInteresUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interes1 = table.Column<int>(type: "int", nullable: false),
                    Interes2 = table.Column<int>(type: "int", nullable: false),
                    Interes3 = table.Column<int>(type: "int", nullable: false),
                    IDUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteresUsuario", x => x.IdInteresUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Libro",
                columns: table => new
                {
                    IdLibro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreLibro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorLibro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcnoLibro = table.Column<int>(type: "int", nullable: false),
                    ImagenLibro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneroLibro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resegna = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paginas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libro", x => x.IdLibro);
                });

            migrationBuilder.CreateTable(
                name: "LibroUsuario",
                columns: table => new
                {
                    IdLibroUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroUsuario", x => x.IdLibroUsuario);
                });

            migrationBuilder.CreateTable(
                name: "ProgresoLectura",
                columns: table => new
                {
                    IdProgresoLectura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    PaginasLeidas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresoLectura", x => x.IdProgresoLectura);
                });

            migrationBuilder.CreateTable(
                name: "ProgresoLibros",
                columns: table => new
                {
                    IdProgreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    PaginasAvance = table.Column<int>(type: "int", nullable: true) 
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresoLibros", x => x.IdProgreso);
                });

            migrationBuilder.CreateTable(
                name: "PuntuacionLibro",
                columns: table => new
                {
                    IdPuntuacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    Puntuacion = table.Column<int>(type: "int", nullable: false),
                    ComentarioLibro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntuacionLibro", x => x.IdPuntuacion);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaveUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Presentacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FechaLectura");

            migrationBuilder.DropTable(
                name: "Intereses");

            migrationBuilder.DropTable(
                name: "InteresUsuario");

            migrationBuilder.DropTable(
                name: "Libro");

            migrationBuilder.DropTable(
                name: "LibroUsuario");

            migrationBuilder.DropTable(
                name: "ProgresoLectura");

            migrationBuilder.DropTable(
                name: "ProgresoLibros");

            migrationBuilder.DropTable(
                name: "PuntuacionLibro");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
