using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingUserandaddingUserImagemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserImageId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserImageId",
                table: "AspNetUsers",
                column: "UserImageId",
                unique: true);

            // Update foreign key to use Cascade Delete when a User is deleted
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserImages_UserImageId",
                table: "AspNetUsers",
                column: "UserImageId",
                principalTable: "UserImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade // Change this to Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserImages_UserImageId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserImageId",
                table: "AspNetUsers");
        }
    }
}
