using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangingEventImageSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventImages_Events_EventId",
                table: "EventImages");

            migrationBuilder.DropIndex(
                name: "IX_EventImages_EventId",
                table: "EventImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EventImages_EventId",
                table: "EventImages",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventImages_Events_EventId",
                table: "EventImages",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
