using Microsoft.EntityFrameworkCore.Migrations;

namespace PS11905_BAODUONG_ASMFULL_NET104.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChiTietHD",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    MaSP = table.Column<int>(nullable: false),
                    IDHoaDon = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NhomSP",
                columns: table => new
                {
                    MaNhom = table.Column<int>(nullable: false),
                    TenNhom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhomSP__234F91CD18CF969F", x => x.MaNhom);
                });

            migrationBuilder.CreateTable(
                name: "SANPHAM",
                columns: table => new
                {
                    MaSP = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSP = table.Column<string>(nullable: false),
                    DonGia = table.Column<decimal>(type: "money", nullable: false),
                    MoTaSP = table.Column<string>(nullable: true),
                    HinhAnh = table.Column<string>(unicode: false, nullable: false),
                    NhomSP = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SANPHAM__2725081CA68D9066", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK__SANPHAM__NhomSP__267ABA7A",
                        column: x => x.NhomSP,
                        principalTable: "NhomSP",
                        principalColumn: "MaNhom",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SANPHAM_NhomSP",
                table: "SANPHAM",
                column: "NhomSP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHD");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "SANPHAM");

            migrationBuilder.DropTable(
                name: "NhomSP");
        }
    }
}
