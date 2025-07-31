using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTestItens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastPayerFullName",
                table: "Gifts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPayerFullName",
                table: "Gifts");
        }
    }
}
