using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserSubscriptionWebApi.Migrations
{
    public partial class finalDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTypes_Name",
                table: "SubscriptionTypes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubscriptionTypes_Name",
                table: "SubscriptionTypes");
        }
    }
}
