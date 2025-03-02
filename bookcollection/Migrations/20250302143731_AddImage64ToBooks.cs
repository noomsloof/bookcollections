using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookcollection.Migrations
{
    /// <inheritdoc />
    public partial class AddImage64ToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Books",
                newName: "Image64");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image64",
                table: "Books",
                newName: "Image");
        }
    }
}
