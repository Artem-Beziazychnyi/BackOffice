using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackOffice.Migrations
{
    public partial class initNewChema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "TimeReseived",
                table: "Brands");

            migrationBuilder.CreateTable(
                name: "BrandQuantitiesTimeReceived",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrandId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    TimeReseived = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandQuantitiesTimeReceived", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandQuantitiesTimeReceived_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandQuantitiesTimeReceived_BrandId",
                table: "BrandQuantitiesTimeReceived",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandQuantitiesTimeReceived");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeReseived",
                table: "Brands",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
