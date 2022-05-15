using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KullaniciDetaylari",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cinsiyet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciDetaylari", x => x.KullaniciId);
                    table.ForeignKey(
                        name: "FK_KullaniciDetaylari_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciDetaylari");
        }
    }
}
