using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Changelengthinuserimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Length",
                table: "UserImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "UserImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
