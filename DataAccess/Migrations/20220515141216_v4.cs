using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar",
                column: "RolId",
                principalTable: "Roller",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar");

            migrationBuilder.AddForeignKey(
                name: "FK_Kullanicilar_Roller_RolId",
                table: "Kullanicilar",
                column: "RolId",
                principalTable: "Roller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
