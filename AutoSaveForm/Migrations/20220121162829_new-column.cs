using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoSaveForm.Migrations
{
    public partial class newcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "registered",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "registered",
                table: "users");
        }
    }
}
