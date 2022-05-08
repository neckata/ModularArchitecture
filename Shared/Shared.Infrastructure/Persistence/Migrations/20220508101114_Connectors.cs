using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamification.Shared.Infrastructure.Persistence.Migrations
{
    public partial class Connectors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Application",
                table: "Connectors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Application",
                table: "Connectors");
        }
    }
}
