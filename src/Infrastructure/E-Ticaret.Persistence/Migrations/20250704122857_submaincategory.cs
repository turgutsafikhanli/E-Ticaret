using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Ticaret.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class submaincategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainCategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_MainCategoryId",
                table: "Categories",
                column: "MainCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_MainCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MainCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "Categories");
        }
    }
}
