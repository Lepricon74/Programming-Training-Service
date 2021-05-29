using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingService.Migrations
{
    public partial class AddTestTopicIdFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tests_TopicId",
                table: "Tests",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Topics_TopicId",
                table: "Tests",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Topics_TopicId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TopicId",
                table: "Tests");
        }
    }
}
