using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquirrelTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Squirrels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squirrels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stashes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SquirrelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stashes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stashes_Squirrels_SquirrelId",
                        column: x => x.SquirrelId,
                        principalTable: "Squirrels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StashLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    StashId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StashLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StashLines_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StashLines_Stashes_StashId",
                        column: x => x.StashId,
                        principalTable: "Stashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A crunchy oak nut packed with energy — perfect for winter storage.", "Acorn" },
                    { 2, "A scaly pine treasure filled with hidden seeds inside.", "Pinecone" },
                    { 3, "A tough shell but worth the effort for the rich nut inside.", "Walnut" },
                    { 4, "A light, twirling seed that’s easy to carry and snack on.", "Maple Seed" },
                    { 5, "Smooth and shiny — a favorite autumn treat for any squirrel.", "Chestnut" },
                    { 6, "A colorful assortment of forest berries, sweet and juicy.", "Berry Mix" },
                    { 7, "Soft and savory, found under the shade of tall trees.", "Mushroom Cap" },
                    { 8, "A small round nut with a mild, buttery flavor.", "Hazelnut" },
                    { 9, "Dried golden kernels, crunchy and filling for long trips.", "Corn Kernel" },
                    { 10, "Tiny but full of flavor and fat — a top choice for energy.", "Sunflower Seed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stashes_SquirrelId",
                table: "Stashes",
                column: "SquirrelId");

            migrationBuilder.CreateIndex(
                name: "IX_StashLines_ItemId",
                table: "StashLines",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StashLines_StashId_ItemId",
                table: "StashLines",
                columns: new[] { "StashId", "ItemId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StashLines");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Stashes");

            migrationBuilder.DropTable(
                name: "Squirrels");
        }
    }
}
