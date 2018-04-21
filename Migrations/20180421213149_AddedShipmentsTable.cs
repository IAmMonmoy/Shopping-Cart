using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Migrations
{
    public partial class AddedShipmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductShipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BuyerAddress = table.Column<string>(nullable: false),
                    BuyerName = table.Column<string>(nullable: false),
                    BuyerPhone = table.Column<string>(nullable: false),
                    TotalCost = table.Column<double>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    isDelivered = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentProductQuantity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductShipmentsId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentProductQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentProductQuantity_ProductShipments_ProductShipmentsId",
                        column: x => x.ProductShipmentsId,
                        principalTable: "ProductShipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentProductQuantity_ProductShipmentsId",
                table: "ShipmentProductQuantity",
                column: "ProductShipmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentProductQuantity");

            migrationBuilder.DropTable(
                name: "ProductShipments");
        }
    }
}
