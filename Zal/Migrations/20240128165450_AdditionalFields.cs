using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zal.Migrations
{
    public partial class AdditionalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nazwisko",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nazwisko",
                table: "AspNetUsers");
        }
    }
}
