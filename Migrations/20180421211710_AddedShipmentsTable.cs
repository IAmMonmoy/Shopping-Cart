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
                name: "shipmentProductQuantity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ShipmentsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipmentProductQuantity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shipmentProductQuantity_ProductShipments_ShipmentsId",
                        column: x => x.ShipmentsId,
                        principalTable: "ProductShipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shipmentProductQuantity_ShipmentsId",
                table: "shipmentProductQuantity",
                column: "ShipmentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shipmentProductQuantity");

            migrationBuilder.DropTable(
                name: "ProductShipments");
        }
    }
}
