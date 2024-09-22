using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBookingWeb_API.Migrations
{
    /// <inheritdoc />
    public partial class addingEventsIdattributeinTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Events_eventsId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "eventsId",
                table: "Tickets",
                newName: "EventsId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_eventsId",
                table: "Tickets",
                newName: "IX_Tickets_EventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Events_EventsId",
                table: "Tickets",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Events_EventsId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "EventsId",
                table: "Tickets",
                newName: "eventsId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_EventsId",
                table: "Tickets",
                newName: "IX_Tickets_eventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Events_eventsId",
                table: "Tickets",
                column: "eventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
