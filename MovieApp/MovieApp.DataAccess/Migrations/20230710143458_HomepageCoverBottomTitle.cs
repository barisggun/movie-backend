using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class HomepageCoverBottomTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BottomTitle",
                table: "HomepageCovers",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BottomTitle",
                table: "HomepageCovers");
        }
    }
}
