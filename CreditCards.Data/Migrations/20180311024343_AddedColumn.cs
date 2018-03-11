using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditCards.Migrations
{
    public partial class AddedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FrequentFlyerNumber",
                table: "CreditCardApplication",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrequentFlyerNumber",
                table: "CreditCardApplication");
        }
    }
}
