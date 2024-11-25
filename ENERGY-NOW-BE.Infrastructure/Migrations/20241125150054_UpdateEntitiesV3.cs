using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENERGY_NOW_BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientSpecialization_ClientConfigurations_ClientId",
                table: "ClientSpecialization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientSpecialization",
                table: "ClientSpecialization");

            migrationBuilder.RenameTable(
                name: "ClientSpecialization",
                newName: "ClientSpecializations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientSpecializations",
                table: "ClientSpecializations",
                columns: new[] { "ClientId", "Specialization" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSpecializations_ClientConfigurations_ClientId",
                table: "ClientSpecializations",
                column: "ClientId",
                principalTable: "ClientConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientSpecializations_ClientConfigurations_ClientId",
                table: "ClientSpecializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientSpecializations",
                table: "ClientSpecializations");

            migrationBuilder.RenameTable(
                name: "ClientSpecializations",
                newName: "ClientSpecialization");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientSpecialization",
                table: "ClientSpecialization",
                columns: new[] { "ClientId", "Specialization" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSpecialization_ClientConfigurations_ClientId",
                table: "ClientSpecialization",
                column: "ClientId",
                principalTable: "ClientConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
