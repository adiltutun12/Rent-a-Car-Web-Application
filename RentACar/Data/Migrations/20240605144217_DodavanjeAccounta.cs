using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    public partial class DodavanjeAccounta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dostava_Racun_DostavljacId",
                table: "Dostava");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Racun_NarucilacId",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Vozila_Poslovnica_Poslovnica",
                table: "Vozila");

            migrationBuilder.DropTable(
                name: "Racun");

            migrationBuilder.AlterColumn<int>(
                name: "Poslovnica",
                table: "Vozila",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NarucilacId",
                table: "Rezervacija",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Dostava_AspNetUsers_DostavljacId",
                table: "Dostava",
                column: "DostavljacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_AspNetUsers_NarucilacId",
                table: "Rezervacija",
                column: "NarucilacId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vozila_Poslovnica_Poslovnica",
                table: "Vozila",
                column: "Poslovnica",
                principalTable: "Poslovnica",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dostava_AspNetUsers_DostavljacId",
                table: "Dostava");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_AspNetUsers_NarucilacId",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Vozila_Poslovnica_Poslovnica",
                table: "Vozila");

            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Poslovnica",
                table: "Vozila",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NarucilacId",
                table: "Rezervacija",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Racun",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Dostava_Racun_DostavljacId",
                table: "Dostava",
                column: "DostavljacId",
                principalTable: "Racun",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Racun_NarucilacId",
                table: "Rezervacija",
                column: "NarucilacId",
                principalTable: "Racun",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vozila_Poslovnica_Poslovnica",
                table: "Vozila",
                column: "Poslovnica",
                principalTable: "Poslovnica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
