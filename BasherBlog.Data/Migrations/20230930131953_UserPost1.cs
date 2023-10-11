using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasherBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserPost1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "postComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_postComments_UserId",
                table: "postComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_postComments_Users_UserId",
                table: "postComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_postComments_Users_UserId",
                table: "postComments");

            migrationBuilder.DropIndex(
                name: "IX_postComments_UserId",
                table: "postComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "postComments");
        }
    }
}
