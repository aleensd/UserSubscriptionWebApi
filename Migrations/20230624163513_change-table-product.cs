using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserSubscriptionWebApi.Migrations
{
    public partial class changetableproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
