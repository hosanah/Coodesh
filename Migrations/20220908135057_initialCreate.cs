using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coodesh.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    PasswordHash = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhoId = table.Column<int>(type: "int", nullable: false),
                    AccessedWhen = table.Column<DateTime>(type: "SMALLDATETIME", maxLength: 60, nullable: false, defaultValueSql: "GETDATE()"),
                    WordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Access_User",
                        column: x => x.WhoId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessHistory_Word_WordId",
                        column: x => x.WordId,
                        principalTable: "Word",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteWord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    When = table.Column<DateTime>(type: "SMALLDATETIME", maxLength: 60, nullable: false, defaultValueSql: "GETDATE()"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    WhoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorite_User",
                        column: x => x.WhoId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorite_Word",
                        column: x => x.WordId,
                        principalTable: "Word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistory_WhoId",
                table: "AccessHistory",
                column: "WhoId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistory_WordId",
                table: "AccessHistory",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteWord_WhoId",
                table: "FavoriteWord",
                column: "WhoId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteWord_WordId",
                table: "FavoriteWord",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessHistory");

            migrationBuilder.DropTable(
                name: "FavoriteWord");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Word");
        }
    }
}
