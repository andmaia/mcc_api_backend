using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class branchHasManyOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "branchId",
                table: "Orders",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_branchId",
                table: "Orders",
                column: "branchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Branch_branchId",
                table: "Orders",
                column: "branchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Branch_branchId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_branchId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "branchId",
                table: "Orders");
        }
    }
}
