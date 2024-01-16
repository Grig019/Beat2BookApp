using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beat2Book.Migrations
{
    /// <inheritdoc />
    public partial class FixingTheApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RaceCategory",
                table: "Races",
                newName: "EventCategory");

            migrationBuilder.RenameColumn(
                name: "ClubCategory",
                table: "Clubs",
                newName: "BandCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventCategory",
                table: "Races",
                newName: "RaceCategory");

            migrationBuilder.RenameColumn(
                name: "BandCategory",
                table: "Clubs",
                newName: "ClubCategory");
        }
    }
}
