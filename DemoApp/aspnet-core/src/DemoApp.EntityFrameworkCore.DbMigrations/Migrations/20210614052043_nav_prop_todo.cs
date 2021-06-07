using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoApp.Migrations
{
    public partial class nav_prop_todo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "ToDos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriorityId1",
                table: "ToDos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId1",
                table: "ToDos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TasksId",
                table: "ToDos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_CategoryId1",
                table: "ToDos",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_PriorityId1",
                table: "ToDos",
                column: "PriorityId1");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_StatusId1",
                table: "ToDos",
                column: "StatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_TasksId",
                table: "ToDos",
                column: "TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Categories_CategoryId1",
                table: "ToDos",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Priorities_PriorityId1",
                table: "ToDos",
                column: "PriorityId1",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Statuses_StatusId1",
                table: "ToDos",
                column: "StatusId1",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Task1s_TasksId",
                table: "ToDos",
                column: "TasksId",
                principalTable: "Task1s",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Categories_CategoryId1",
                table: "ToDos");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Priorities_PriorityId1",
                table: "ToDos");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Statuses_StatusId1",
                table: "ToDos");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Task1s_TasksId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_CategoryId1",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_PriorityId1",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_StatusId1",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_TasksId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "PriorityId1",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "TasksId",
                table: "ToDos");
        }
    }
}
