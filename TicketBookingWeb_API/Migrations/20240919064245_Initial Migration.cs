using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketBookingWeb_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventVenue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketPrice = table.Column<int>(type: "int", nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketQty = table.Column<int>(type: "int", nullable: false),
                    eventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Events_eventsId",
                        column: x => x.eventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "DateAndTime", "EventDescription", "EventName", "EventVenue", "TicketPrice" },
                values: new object[,]
                {
                    { new Guid("362676cd-ff9e-4274-b275-32cce4258ef8"), "30-09-2024 12:00 PM", "Explore gourmet food and exquisite wines from around the world.", "Food Mela", "Noida Expo Centre, Noida", 500 },
                    { new Guid("6434842b-01c6-482e-aefd-937205a787b3"), "01-10-2024 12:00 PM", "Celebrate the innovative tech startups at TechCrunch Disrupt", "TechCrunch Disrupt", "Manekshaw Centre, Delhi", 2000 },
                    { new Guid("bd759470-fed0-4b95-b0f5-602959f29682"), "23-09-2024 12:00 PM", "A leading conference on emerging technologies including AI, Blockchain, and Quantum Computing.", "Tech Conference 2024", "Bharat Mandapam, Pragati Maidan, Delhi", 2000 },
                    { new Guid("dbe68caf-5b9f-4af9-971a-93f65f15b221"), "26-10-2024 7:00 PM", "A night of live music performances by Diljit Dosanjh", "Dilluminati", "JLN Stadium, Delhi", 13000 },
                    { new Guid("f5f64c28-64d0-4775-b244-a708ea53b728"), "25-09-2024 6:00 PM", "Laughout Loud with Zakir Khan's crazy anecdotes", "Comedy Night with Zakir Khan", "Comedy Club, Gurgaon", 1500 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_eventsId",
                table: "Tickets",
                column: "eventsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
