using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinderAlbania.Data.Migrations
{
    /// <inheritdoc />
    public partial class bio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AspNetUsers",
                newName: "Bio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "AspNetUsers",
                newName: "Description");
        }
    }
}
