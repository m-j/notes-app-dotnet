using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesAppDotnet.Migrations
{
    public partial class Adding_tag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Notes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Notes");
        }
    }
}
