using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeRecord.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Task_Name = table.Column<string>(maxLength: 100, nullable: false),
                    Task_Description = table.Column<string>(maxLength: 100, nullable: false),
                    Project_Name = table.Column<string>(maxLength: 100, nullable: false),
                    Client_Name = table.Column<string>(maxLength: 100, nullable: false),
                    Start_Time = table.Column<DateTime>(nullable: false),
                    End_Time = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");
        }
    }
}
