using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENERGY_NOW_BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "ClientConfigurations",
                newName: "Icon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "ClientConfigurations",
                newName: "Avatar");
        }
    }
}
