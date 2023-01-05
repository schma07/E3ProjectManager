using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schma.E3ProjectManager.Infrastructure.Migrations.Application
{
    /// <inheritdoc />
    public partial class updateProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Revision",
                schema: "Domain",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                schema: "Domain",
                table: "ProjectDevices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revision",
                schema: "Domain",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                schema: "Domain",
                table: "ProjectDevices",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
