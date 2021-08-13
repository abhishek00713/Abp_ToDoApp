using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoApp.Migrations
{
    public partial class added_cganges_appUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "AssignedToUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedToUsers_AppUserId",
                table: "AssignedToUsers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedToUsers_AppUser_AppUserId",
                table: "AssignedToUsers",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedToUsers_AppUser_AppUserId",
                table: "AssignedToUsers");

            migrationBuilder.DropIndex(
                name: "IX_AssignedToUsers_AppUserId",
                table: "AssignedToUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AssignedToUsers");
        }
    }
}
