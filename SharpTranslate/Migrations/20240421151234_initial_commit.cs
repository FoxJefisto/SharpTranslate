using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SharpTranslate.Migrations
{
    public partial class initial_commit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WordName = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_word", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wordpair",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OriginalWordId = table.Column<int>(type: "integer", nullable: false),
                    TranslationWordId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wordpair", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wordpair_word_OriginalWordId",
                        column: x => x.OriginalWordId,
                        principalTable: "word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wordpair_word_TranslationWordId",
                        column: x => x.TranslationWordId,
                        principalTable: "word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_wordpair",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    WordPairId = table.Column<int>(type: "integer", nullable: false),
                    WordStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_wordpair", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_wordpair_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_wordpair_wordpair_WordPairId",
                        column: x => x.WordPairId,
                        principalTable: "wordpair",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_wordpair_UserId",
                table: "user_wordpair",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_wordpair_WordPairId",
                table: "user_wordpair",
                column: "WordPairId");

            migrationBuilder.CreateIndex(
                name: "IX_wordpair_OriginalWordId",
                table: "wordpair",
                column: "OriginalWordId");

            migrationBuilder.CreateIndex(
                name: "IX_wordpair_TranslationWordId",
                table: "wordpair",
                column: "TranslationWordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_wordpair");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "wordpair");

            migrationBuilder.DropTable(
                name: "word");
        }
    }
}
