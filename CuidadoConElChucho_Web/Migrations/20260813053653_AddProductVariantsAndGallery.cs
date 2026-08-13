using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuidadoConElChucho_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddProductVariantsAndGallery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductVariations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "ProductImages",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductVariations");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "ProductImages");
        }
    }
}
