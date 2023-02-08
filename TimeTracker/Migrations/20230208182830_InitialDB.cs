using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClockworkAccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    AuthorizationToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "ClockworkTask",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClockworkTaskId = table.Column<int>(type: "int", nullable: false),
                    ClockworkTaskKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSpentSeconds = table.Column<TimeSpan>(type: "time", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClockworkTask", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_ClockworkTask_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "AccountId1", "AuthorizationToken", "ClockworkAccountId", "FirstName", "LastName", "Password", "Type", "UserName" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), null, null, "unknown", null, null, "adminadmin", 0, "admin" });

            migrationBuilder.InsertData(
                table: "ClockworkTask",
                columns: new[] { "TaskId", "AccountId", "ClockworkTaskId", "ClockworkTaskKey", "StartedDateTime", "TimeSpentSeconds" },
                values: new object[] { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 0, "unknown", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountId1",
                table: "Accounts",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClockworkTask_AccountId",
                table: "ClockworkTask",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClockworkTask");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
