using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrameworkLayer.Migrations
{
    /// <inheritdoc />
    public partial class DT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewTestEntity",
                columns: table => new
                {
                    ProductCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ProductDescription = table.Column<string>(type: "text", nullable: false),
                    QuantitySold = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    TimeAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewTestEntity", x => new { x.ProductCode, x.ProductDescription });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewTestEntity");
        }
    }
}
