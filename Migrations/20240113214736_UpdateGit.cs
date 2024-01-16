using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beat2Book.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatedProjectPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedProjectPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedProjectPhoto_Races_EventId",
                        column: x => x.EventId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedProjectPhoto_EventId",
                table: "RelatedProjectPhoto",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedProjectPhoto");
        }
    }
}
