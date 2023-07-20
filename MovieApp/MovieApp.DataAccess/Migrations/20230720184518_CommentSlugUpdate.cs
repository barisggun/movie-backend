﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CommentSlugUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieSlug",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieSlug",
                table: "Comments");
        }
    }
}
