using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPayerEmailToGiftOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayerEmail",
                table: "GiftOrders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayerEmail",
                table: "GiftOrders");
        }
    }
}
