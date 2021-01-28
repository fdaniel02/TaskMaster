using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.Domain.Migrations
{
    public partial class AddClosedState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProjectState",
                columns: new[] { "ID", "Name" },
                values: new object[] { 9, "Closed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectState",
                keyColumn: "ID",
                keyValue: 9);
        }
    }
}
