using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termin_AspNetUsers_PacijentId",
                table: "Termin");

            migrationBuilder.AlterColumn<string>(
                name: "DoktorId",
                table: "Termin",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Termin_DoktorId",
                table: "Termin",
                column: "DoktorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Termin_AspNetUsers_DoktorId",
                table: "Termin",
                column: "DoktorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Termin_AspNetUsers_PacijentId",
                table: "Termin",
                column: "PacijentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Termin_AspNetUsers_DoktorId",
                table: "Termin");

            migrationBuilder.DropForeignKey(
                name: "FK_Termin_AspNetUsers_PacijentId",
                table: "Termin");

            migrationBuilder.DropIndex(
                name: "IX_Termin_DoktorId",
                table: "Termin");

            migrationBuilder.AlterColumn<int>(
                name: "DoktorId",
                table: "Termin",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Termin_AspNetUsers_PacijentId",
                table: "Termin",
                column: "PacijentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
