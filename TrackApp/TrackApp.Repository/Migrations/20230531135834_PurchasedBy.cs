using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class PurchasedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PurchasedBy",
                table: "Purchases",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasedBy",
                table: "Purchases");
        }
    }
}
