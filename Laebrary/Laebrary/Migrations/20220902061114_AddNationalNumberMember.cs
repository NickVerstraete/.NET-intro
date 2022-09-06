using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laebrary.Migrations
{
    public partial class AddNationalNumberMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalNumber",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalNumber",
                table: "Members");
        }
    }
}
