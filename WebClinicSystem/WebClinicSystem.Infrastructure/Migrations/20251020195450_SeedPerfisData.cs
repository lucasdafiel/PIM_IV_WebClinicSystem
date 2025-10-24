using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebClinicSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPerfisData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Perfis",
                columns: new[] { "PerfilId", "Nome" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Profissional de Saúde" },
                    { 3, "Recepcionista" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Perfis",
                keyColumn: "PerfilId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Perfis",
                keyColumn: "PerfilId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Perfis",
                keyColumn: "PerfilId",
                keyValue: 3);
        }
    }
}
