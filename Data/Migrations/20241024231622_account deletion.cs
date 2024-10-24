using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinderAlbania.Data.Migrations
{
    /// <inheritdoc />
    public partial class accountdeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Buyer_Rating",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "AccountDeletionRequested",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountDeletionRequested",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<double>(
                name: "Buyer_Rating",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "AspNetUsers",
                type: "float",
                nullable: true);
        }
    }
}
