using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taxi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyRides : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rides_user_id",
                table: "rides");

            migrationBuilder.CreateIndex(
                name: "ix_rides_user_id",
                table: "rides",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_rides_user_user_id",
                table: "rides",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_rides_user_user_id",
                table: "rides");

            migrationBuilder.DropIndex(
                name: "ix_rides_user_id",
                table: "rides");

            migrationBuilder.AddForeignKey(
                name: "fk_rides_user_id",
                table: "rides",
                column: "id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
