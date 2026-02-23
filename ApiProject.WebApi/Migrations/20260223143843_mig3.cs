using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProject.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Testimonials",
                newName: "NameSurname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Testimonials",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameSurname",
                table: "Testimonials",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Testimonials",
                newName: "Name");
        }
    }
}
