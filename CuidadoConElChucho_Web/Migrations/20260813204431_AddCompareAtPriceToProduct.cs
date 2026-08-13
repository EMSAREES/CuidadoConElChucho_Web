using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuidadoConElChucho_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddCompareAtPriceToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CompareAtPrice",
                table: "Products",
                type: "decimal(10,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompareAtPrice",
                table: "Products");
        }
    }
}
