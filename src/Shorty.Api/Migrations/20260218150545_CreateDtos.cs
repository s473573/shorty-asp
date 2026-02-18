using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shorty.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateDtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clicks_Links_LinkId",
                table: "Clicks");

            migrationBuilder.AddForeignKey(
                name: "FK_Clicks_Links_LinkId",
                table: "Clicks",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clicks_Links_LinkId",
                table: "Clicks");

            migrationBuilder.AddForeignKey(
                name: "FK_Clicks_Links_LinkId",
                table: "Clicks",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
