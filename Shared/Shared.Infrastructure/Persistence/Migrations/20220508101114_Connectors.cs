using Microsoft.EntityFrameworkCore.Migrations;

namespace ModularArchitecture.Shared.Infrastructure.Persistence.Migrations
{
    public partial class Modules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Application",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Application",
                table: "Modules");
        }
    }
}
