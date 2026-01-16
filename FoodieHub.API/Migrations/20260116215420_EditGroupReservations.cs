using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodieHub.API.Migrations
{
    public partial class EditGroupReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersoToContact",
                table: "GroupReservations",
                newName: "PersonToContact");

            migrationBuilder.RenameColumn(
                name: "GorupTitle",
                table: "GroupReservations",
                newName: "GroupTitle");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "GroupReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PersonCount",
                table: "GroupReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "GroupReservations");

            migrationBuilder.DropColumn(
                name: "PersonCount",
                table: "GroupReservations");

            migrationBuilder.RenameColumn(
                name: "PersonToContact",
                table: "GroupReservations",
                newName: "PersoToContact");

            migrationBuilder.RenameColumn(
                name: "GroupTitle",
                table: "GroupReservations",
                newName: "GorupTitle");
        }
    }
}
