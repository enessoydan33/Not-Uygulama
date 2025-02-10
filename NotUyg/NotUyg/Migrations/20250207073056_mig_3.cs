using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotUyg.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Not_NotId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Tag_TagId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_NotId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_TagId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "NotId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "NotTag",
                columns: table => new
                {
                    NotsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotTag", x => new { x.NotsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_NotTag_Not_NotsId",
                        column: x => x.NotsId,
                        principalTable: "Not",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotTag_TagsId",
                table: "NotTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotTag");

            migrationBuilder.AddColumn<int>(
                name: "NotId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_NotId",
                table: "Tag",
                column: "NotId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_TagId",
                table: "Tag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Not_NotId",
                table: "Tag",
                column: "NotId",
                principalTable: "Not",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Tag_TagId",
                table: "Tag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id");
        }
    }
}
