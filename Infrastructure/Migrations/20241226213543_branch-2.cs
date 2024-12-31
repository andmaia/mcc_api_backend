using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class branch2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Companies_companyId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Branch_branchId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Comissions_ComissionId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branch",
                table: "Branch");

            migrationBuilder.RenameTable(
                name: "Branch",
                newName: "Branchs");

            migrationBuilder.RenameIndex(
                name: "IX_Branch_companyId",
                table: "Branchs",
                newName: "IX_Branchs_companyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branchs",
                table: "Branchs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branchs_Companies_companyId",
                table: "Branchs",
                column: "companyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Branchs_branchId",
                table: "Orders",
                column: "branchId",
                principalTable: "Branchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Comissions_ComissionId",
                table: "Orders",
                column: "ComissionId",
                principalTable: "Comissions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branchs_Companies_companyId",
                table: "Branchs");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Branchs_branchId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Comissions_ComissionId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branchs",
                table: "Branchs");

            migrationBuilder.RenameTable(
                name: "Branchs",
                newName: "Branch");

            migrationBuilder.RenameIndex(
                name: "IX_Branchs_companyId",
                table: "Branch",
                newName: "IX_Branch_companyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branch",
                table: "Branch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Companies_companyId",
                table: "Branch",
                column: "companyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Branch_branchId",
                table: "Orders",
                column: "branchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Comissions_ComissionId",
                table: "Orders",
                column: "ComissionId",
                principalTable: "Comissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
