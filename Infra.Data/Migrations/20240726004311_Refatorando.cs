using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refatorando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transações_Contas_ContaId",
                table: "Transações");

            migrationBuilder.RenameColumn(
                name: "ContaId",
                table: "Transações",
                newName: "ContaDestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_Transações_ContaId",
                table: "Transações",
                newName: "IX_Transações_ContaDestinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transações_Contas_ContaDestinoId",
                table: "Transações",
                column: "ContaDestinoId",
                principalTable: "Contas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transações_Contas_ContaDestinoId",
                table: "Transações");

            migrationBuilder.RenameColumn(
                name: "ContaDestinoId",
                table: "Transações",
                newName: "ContaId");

            migrationBuilder.RenameIndex(
                name: "IX_Transações_ContaDestinoId",
                table: "Transações",
                newName: "IX_Transações_ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transações_Contas_ContaId",
                table: "Transações",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id");
        }
    }
}
