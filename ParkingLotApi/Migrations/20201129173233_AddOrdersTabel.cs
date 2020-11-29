using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotApi.Migrations
{
    public partial class AddOrdersTabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderNumber = table.Column<string>(nullable: false),
                    ParkingLotName = table.Column<string>(nullable: true),
                    PlateNumber = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CloseTime = table.Column<DateTime>(nullable: true),
                    IsOpen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
