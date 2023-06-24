using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserSubscriptionWebApi.Migrations
{
    public partial class changetablesubscriptiontype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "SubscriptionTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "SubscriptionTypes");
        }
    }
}
