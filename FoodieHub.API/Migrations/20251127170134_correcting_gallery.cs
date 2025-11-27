using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodieHub.API.Migrations
{
    public partial class correcting_gallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Galleries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
