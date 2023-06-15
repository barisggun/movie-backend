using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class appuserAddedBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUsersId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AppUsersId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AppUsersId",
                table: "Blogs",
                newName: "AppUserId1");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AppUserId",
                table: "Blogs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AppUserId1",
                table: "Blogs",
                column: "AppUserId1",
                unique: true,
                filter: "[AppUserId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUserId",
                table: "Blogs",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUserId1",
                table: "Blogs",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUserId1",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AppUserId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AppUserId1",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "AppUserId1",
                table: "Blogs",
                newName: "AppUsersId");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Blogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AppUsersId",
                table: "Blogs",
                column: "AppUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_AppUsersId",
                table: "Blogs",
                column: "AppUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
