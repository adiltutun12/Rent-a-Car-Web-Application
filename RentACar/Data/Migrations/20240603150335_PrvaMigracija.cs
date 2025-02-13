using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    public partial class PrvaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poslovnica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kontakt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RadnoVrijeme = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslovnica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Racun",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Racun_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vozila",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Proizvodjac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    RegistarskeTablice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Navigacija = table.Column<bool>(type: "bit", nullable: false),
                    Transmisija = table.Column<int>(type: "int", nullable: false),
                    Gorivo = table.Column<int>(type: "int", nullable: false),
                    Poslovnica = table.Column<int>(type: "int", nullable: false),
                    Dostupno = table.Column<bool>(type: "bit", nullable: false),
                    PutnickoVozilo_Tip = table.Column<int>(type: "int", nullable: true),
                    BrojSjedista = table.Column<int>(type: "int", nullable: true),
                    Tempomat = table.Column<bool>(type: "bit", nullable: true),
                    TransportnoVozilo_Tip = table.Column<int>(type: "int", nullable: true),
                    Nosivost = table.Column<double>(type: "float", nullable: true),
                    Duzina = table.Column<double>(type: "float", nullable: true),
                    Prikolica = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozila", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vozila_Poslovnica_Poslovnica",
                        column: x => x.Poslovnica,
                        principalTable: "Poslovnica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumRezervacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumPreuzimanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumPovratka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Iznos = table.Column<double>(type: "float", nullable: false),
                    VoziloId = table.Column<int>(type: "int", nullable: false),
                    NarucilacId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VrstaPlacanja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Racun_NarucilacId",
                        column: x => x.NarucilacId,
                        principalTable: "Racun",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Dostava",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NarudzbaId = table.Column<int>(type: "int", nullable: false),
                    DostavljacId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prihvacena = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostava", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dostava_Racun_DostavljacId",
                        column: x => x.DostavljacId,
                        principalTable: "Racun",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Dostava_Rezervacija_NarudzbaId",
                        column: x => x.NarudzbaId,
                        principalTable: "Rezervacija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dostava_DostavljacId",
                table: "Dostava",
                column: "DostavljacId");

            migrationBuilder.CreateIndex(
                name: "IX_Dostava_NarudzbaId",
                table: "Dostava",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_NarucilacId",
                table: "Rezervacija",
                column: "NarucilacId");

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_Poslovnica",
                table: "Vozila",
                column: "Poslovnica");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dostava");

            migrationBuilder.DropTable(
                name: "Vozila");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Poslovnica");

            migrationBuilder.DropTable(
                name: "Racun");
        }
    }
}
