using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDetails.Migrations
{
    public partial class pp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Frasher",
                table: "candidates",
                newName: "Fresher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fresher",
                table: "candidates",
                newName: "Frasher");
        }
    }
}
