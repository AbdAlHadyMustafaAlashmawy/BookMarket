using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMarket.Migrations
{
    /// <inheritdoc />
    public partial class cost_Bag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<double>(
                name: "cost",
                table: "Bag",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<bool>(
                name: "Admin",
                table: "Accounts",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cost",
                table: "Bag");

        }
    }
}
