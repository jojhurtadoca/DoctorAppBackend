using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTablePatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CreatedById",
                table: "Patients",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UpdatedById",
                table: "Patients",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_CreatedById",
                table: "Patients",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_UpdatedById",
                table: "Patients",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_CreatedById",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_UpdatedById",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_CreatedById",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_UpdatedById",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Patients");
        }
    }
}
