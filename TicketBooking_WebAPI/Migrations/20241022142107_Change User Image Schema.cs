using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserImageSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImages_AspNetUsers_UserId",
                table: "UserImages");

            migrationBuilder.DropIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserImages");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserImageId",
                table: "AspNetUsers",
                column: "UserImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserImages_UserImageId",
                table: "AspNetUsers",
                column: "UserImageId",
                principalTable: "UserImages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserImages_UserImageId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserImageId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserImages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserImages_AspNetUsers_UserId",
                table: "UserImages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
