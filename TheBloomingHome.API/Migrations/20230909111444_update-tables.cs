using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBloomingHome.API.Migrations
{
    /// <inheritdoc />
    public partial class updatetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Products_ProductId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Feature_Products_ProductId",
                table: "Feature");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Products_ProductId",
                table: "Property");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property",
                table: "Property");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feature",
                table: "Feature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Property",
                newName: "Stats");

            migrationBuilder.RenameTable(
                name: "Feature",
                newName: "Features");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Property_ProductId",
                table: "Stats",
                newName: "IX_Stats_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Feature_ProductId",
                table: "Features",
                newName: "IX_Features_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ProductId",
                table: "Comments",
                newName: "IX_Comments_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stats",
                table: "Stats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Features",
                table: "Features",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Products_ProductId",
                table: "Features",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Products_ProductId",
                table: "Stats",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Products_ProductId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Products_ProductId",
                table: "Stats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stats",
                table: "Stats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Features",
                table: "Features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Stats",
                newName: "Property");

            migrationBuilder.RenameTable(
                name: "Features",
                newName: "Feature");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Stats_ProductId",
                table: "Property",
                newName: "IX_Property_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Features_ProductId",
                table: "Feature",
                newName: "IX_Feature_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProductId",
                table: "Comment",
                newName: "IX_Comment_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property",
                table: "Property",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feature",
                table: "Feature",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Products_ProductId",
                table: "Comment",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_Products_ProductId",
                table: "Feature",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Products_ProductId",
                table: "Property",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
