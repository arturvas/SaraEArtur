using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.API.Migrations
{
    /// <inheritdoc />
    public partial class FixNameAndSurnameToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayerFirstName",
                table: "GiftOrders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayerLastName",
                table: "GiftOrders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayerFirstName",
                table: "GiftOrders");

            migrationBuilder.DropColumn(
                name: "PayerLastName",
                table: "GiftOrders");
        }
    }
}
