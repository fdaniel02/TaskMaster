using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.Domain.Migrations
{
    public partial class DefaultStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProjectState",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Inbox" },
                    { 2, "Next" },
                    { 3, "Scheduled" },
                    { 4, "Waiting" },
                    { 5, "Delegated" },
                    { 6, "Later" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectState",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectState",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectState",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectState",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectState",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProjectState",
                keyColumn: "ID",
                keyValue: 6);
        }
    }
}
