using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taxi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mrs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_types",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "user_type",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_type",
                table: "users");

            migrationBuilder.AddColumn<int[]>(
                name: "user_types",
                table: "users",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }
    }
}
