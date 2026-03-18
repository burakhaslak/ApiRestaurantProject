using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProject.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeTaskId",
                table: "EmployeeTasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignDate",
                table: "EmployeeTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "EmployeeTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "EmployeeTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaskName",
                table: "EmployeeTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaskStatus",
                table: "EmployeeTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "TaskStatusValue",
                table: "EmployeeTasks",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);


            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTasks",
                table: "EmployeeTasks",
                column: "EmployeeTaskId");

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTaskChefs_Chefs_ChefId",
                table: "EmployeeTaskChefs");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTaskChefs_EmployeeTasks_EmployeeTaskId",
                table: "EmployeeTaskChefs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTasks",
                table: "EmployeeTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTaskChefs",
                table: "EmployeeTaskChefs");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTaskChefs_ChefId",
                table: "EmployeeTaskChefs");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTaskChefs_EmployeeTaskId",
                table: "EmployeeTaskChefs");

            migrationBuilder.DropColumn(
                name: "AssignDate",
                table: "EmployeeTasks");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "EmployeeTasks");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "EmployeeTasks");

            migrationBuilder.DropColumn(
                name: "TaskName",
                table: "EmployeeTasks");

            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "EmployeeTasks");

            migrationBuilder.DropColumn(
                name: "TaskStatusValue",
                table: "EmployeeTasks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeeTaskChefs");

            migrationBuilder.RenameTable(
                name: "EmployeeTasks",
                newName: "EmployeeTask");

            migrationBuilder.RenameTable(
                name: "EmployeeTaskChefs",
                newName: "EmployeeTaskChef");

            migrationBuilder.RenameColumn(
                name: "EmployeeTaskId",
                table: "EmployeeTask",
                newName: "TempId");

            migrationBuilder.AlterColumn<int>(
                name: "TempId",
                table: "EmployeeTask",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeTaskId",
                table: "EmployeeTaskChef",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ChefId",
                table: "EmployeeTaskChef",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmployeeTask_TempId",
                table: "EmployeeTask",
                column: "TempId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTaskChef_Chefs_ChefId",
                table: "EmployeeTaskChef",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "ChefId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTaskChef_EmployeeTask_EmployeeTaskId",
                table: "EmployeeTaskChef",
                column: "EmployeeTaskId",
                principalTable: "EmployeeTask",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
