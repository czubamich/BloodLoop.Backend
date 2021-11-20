using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodLoop.Infrastructure.Migrations
{
    public partial class BloodType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Donors");
        }
    }
}
