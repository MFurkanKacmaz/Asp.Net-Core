using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPortFolio.Migrations
{
    /// <inheritdoc />
    public partial class updateResumeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Resumes");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndYear",
                table: "Resumes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartYear",
                table: "Resumes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "StartYear",
                table: "Resumes");

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
