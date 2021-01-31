using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMaster.Domain.Migrations
{
    public partial class ProjectStateTableRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectState_StateID",
                table: "Project");

            migrationBuilder.DropTable(
                name: "ProjectState");

            migrationBuilder.DropIndex(
                name: "IX_Project_StateID",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StateID",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectState",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectState", x => x.ID);
                });

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
                    { 6, "Later" },
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_StateID",
                table: "Project",
                column: "StateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectState_StateID",
                table: "Project",
                column: "StateID",
                principalTable: "ProjectState",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}