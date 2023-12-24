using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPortFolio.Migrations
{
    /// <inheritdoc />
    public partial class updateResumeColumnNametoDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Resumes",
                newName: "Department");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Resumes",
                newName: "Title");
        }
    }
}
