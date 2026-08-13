using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuidadoConElChucho_Web.Migrations
{
    /// <inheritdoc />
    public partial class RenameCompareAtPriceToSalePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompareAtPrice",
                table: "Products",
                newName: "SalePrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalePrice",
                table: "Products",
                newName: "CompareAtPrice");
        }
    }
}
