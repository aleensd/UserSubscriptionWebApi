using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserSubscriptionWebApi.Migrations
{
    public partial class altertableproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
