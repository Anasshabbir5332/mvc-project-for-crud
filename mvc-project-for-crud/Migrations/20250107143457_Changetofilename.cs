using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvc_project_for_crud.Migrations
{
    /// <inheritdoc />
    public partial class Changetofilename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFulename",
                table: "products",
                newName: "ImageFilename");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFilename",
                table: "products",
                newName: "ImageFulename");
        }
    }
}
