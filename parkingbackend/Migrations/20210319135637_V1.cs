using Microsoft.EntityFrameworkCore.Migrations;

namespace parkingbackend.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    N = table.Column<int>(type: "int", nullable: false),
                    M = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Polje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false),
                    Boja = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Statusp = table.Column<int>(type: "int", nullable: false),
                    Tablice = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Vreme = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Imeprezime = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Marka = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ParkingID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Polje_Parking_ParkingID",
                        column: x => x.ParkingID,
                        principalTable: "Parking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polje_ParkingID",
                table: "Polje",
                column: "ParkingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Polje");

            migrationBuilder.DropTable(
                name: "Parking");
        }
    }
}
