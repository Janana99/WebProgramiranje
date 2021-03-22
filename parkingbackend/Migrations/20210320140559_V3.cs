using Microsoft.EntityFrameworkCore.Migrations;

namespace parkingbackend.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imeprezime",
                table: "Polje");

            migrationBuilder.DropColumn(
                name: "Marka",
                table: "Polje");

            migrationBuilder.DropColumn(
                name: "Tablice",
                table: "Polje");

            migrationBuilder.RenameColumn(
                name: "Vreme",
                table: "Polje",
                newName: "Nazivpolja");

            migrationBuilder.RenameColumn(
                name: "Statusp",
                table: "Polje",
                newName: "Maxkapacitet");

            migrationBuilder.RenameColumn(
                name: "Counter",
                table: "Polje",
                newName: "Idparkinga");

            migrationBuilder.AddColumn<int>(
                name: "Brojautomobila",
                table: "Polje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Automobil",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idpolja = table.Column<int>(type: "int", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Automobilibroj = table.Column<int>(type: "int", nullable: false),
                    PoljeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobil", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Automobil_Polje_PoljeID",
                        column: x => x.PoljeID,
                        principalTable: "Polje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobil_PoljeID",
                table: "Automobil",
                column: "PoljeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automobil");

            migrationBuilder.DropColumn(
                name: "Brojautomobila",
                table: "Polje");

            migrationBuilder.RenameColumn(
                name: "Nazivpolja",
                table: "Polje",
                newName: "Vreme");

            migrationBuilder.RenameColumn(
                name: "Maxkapacitet",
                table: "Polje",
                newName: "Statusp");

            migrationBuilder.RenameColumn(
                name: "Idparkinga",
                table: "Polje",
                newName: "Counter");

            migrationBuilder.AddColumn<string>(
                name: "Imeprezime",
                table: "Polje",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marka",
                table: "Polje",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tablice",
                table: "Polje",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
