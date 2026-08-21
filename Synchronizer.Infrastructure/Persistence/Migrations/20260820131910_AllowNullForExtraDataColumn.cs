using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synchronizer.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullForExtraDataColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExtraData",
                table: "Product",
                type: "JSON",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "JSON");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExtraData",
                table: "Product",
                type: "JSON",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "JSON",
                oldNullable: true);
        }
    }
}
