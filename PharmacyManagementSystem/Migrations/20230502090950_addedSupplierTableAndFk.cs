using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagementSystem.Migrations
{
    public partial class addedSupplierTableAndFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "supplier_email",
                table: "Drug",
                newName: "SupplierId");

            migrationBuilder.AddColumn<int>(
                name: "SupplierDetailId",
                table: "Drug",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SupplierDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplier_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    supplier_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplier_address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    supplier_phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drug_SupplierDetailId",
                table: "Drug",
                column: "SupplierDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drug_SupplierDetails_SupplierDetailId",
                table: "Drug",
                column: "SupplierDetailId",
                principalTable: "SupplierDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drug_SupplierDetails_SupplierDetailId",
                table: "Drug");

            migrationBuilder.DropTable(
                name: "SupplierDetails");

            migrationBuilder.DropIndex(
                name: "IX_Drug_SupplierDetailId",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "SupplierDetailId",
                table: "Drug");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Drug",
                newName: "supplier_email");
        }
    }
}
