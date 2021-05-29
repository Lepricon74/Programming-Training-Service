using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingService.Migrations
{
    public partial class DeleteTestTopicColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Tests");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
