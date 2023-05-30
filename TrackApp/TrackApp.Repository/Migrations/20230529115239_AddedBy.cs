using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "ItemLists",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "ItemLists");
        }
    }
}
