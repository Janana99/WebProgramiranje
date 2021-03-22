using Microsoft.EntityFrameworkCore.Migrations;

namespace parkingbackend.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idpolja",
                table: "Parking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Idpolja",
                table: "Parking",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
