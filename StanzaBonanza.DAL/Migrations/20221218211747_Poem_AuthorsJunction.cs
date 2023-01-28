using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StanzaBonanza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PoemAuthorsJunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorPoem");

            migrationBuilder.CreateTable(
                name: "Poem_Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoemId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poem_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poem_Authors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Poem_Authors_Poems_PoemId",
                        column: x => x.PoemId,
                        principalTable: "Poems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Poem_Authors_AuthorId",
                table: "Poem_Authors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Poem_Authors_PoemId",
                table: "Poem_Authors",
                column: "PoemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poem_Authors");

            migrationBuilder.CreateTable(
                name: "AuthorPoem",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    PoemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorPoem", x => new { x.AuthorsId, x.PoemsId });
                    table.ForeignKey(
                        name: "FK_AuthorPoem_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorPoem_Poems_PoemsId",
                        column: x => x.PoemsId,
                        principalTable: "Poems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorPoem_PoemsId",
                table: "AuthorPoem",
                column: "PoemsId");
        }
    }
}
