using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AfvalScheiden.Data.Migrations
{
    /// <inheritdoc />
    public partial class AfvalDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarbageCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarbageCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Governments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GovernmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Governments_GovernmentId",
                        column: x => x.GovernmentId,
                        principalTable: "Governments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logbooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logbooks_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Garbages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeNeeded = table.Column<int>(type: "int", nullable: false),
                    GarbageCategoryId = table.Column<int>(type: "int", nullable: false),
                    LogbookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garbages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Garbages_GarbageCategories_GarbageCategoryId",
                        column: x => x.GarbageCategoryId,
                        principalTable: "GarbageCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Garbages_Logbooks_LogbookId",
                        column: x => x.LogbookId,
                        principalTable: "Logbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Garbages_GarbageCategoryId",
                table: "Garbages",
                column: "GarbageCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Garbages_LogbookId",
                table: "Garbages",
                column: "LogbookId");

            migrationBuilder.CreateIndex(
                name: "IX_Logbooks_PersonId",
                table: "Logbooks",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GovernmentId",
                table: "Persons",
                column: "GovernmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Garbages");

            migrationBuilder.DropTable(
                name: "GarbageCategories");

            migrationBuilder.DropTable(
                name: "Logbooks");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Governments");
        }
    }
}
