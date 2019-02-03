using Microsoft.EntityFrameworkCore.Migrations;

namespace ASR.Data.Migrations
{
    public partial class StaffNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slot_AspNetUsers_StaffID",
                table: "Slot");

            migrationBuilder.AlterColumn<string>(
                name: "StaffID",
                table: "Slot",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Slot_AspNetUsers_StaffID",
                table: "Slot",
                column: "StaffID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slot_AspNetUsers_StaffID",
                table: "Slot");

            migrationBuilder.AlterColumn<string>(
                name: "StaffID",
                table: "Slot",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Slot_AspNetUsers_StaffID",
                table: "Slot",
                column: "StaffID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
