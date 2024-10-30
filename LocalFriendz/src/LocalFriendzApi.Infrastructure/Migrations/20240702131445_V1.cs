using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalFriendzApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_AREA_CODE",
                columns: table => new
                {
                    id_area_code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code_region = table.Column<string>(type: "VARCHAR(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AREA_CODE", x => x.id_area_code);
                });

            migrationBuilder.CreateTable(
                name: "TB_CONTACT",
                columns: table => new
                {
                    id_contact = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "VARCHAR(40)", maxLength: 40, nullable: false),
                    AreaCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CONTACT", x => x.id_contact);
                    table.ForeignKey(
                        name: "FK_TB_CONTACT_TB_AREA_CODE_AreaCodeId",
                        column: x => x.AreaCodeId,
                        principalTable: "TB_AREA_CODE",
                        principalColumn: "id_area_code");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CONTACT_AreaCodeId",
                table: "TB_CONTACT",
                column: "AreaCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CONTACT");

            migrationBuilder.DropTable(
                name: "TB_AREA_CODE");
        }
    }
}
