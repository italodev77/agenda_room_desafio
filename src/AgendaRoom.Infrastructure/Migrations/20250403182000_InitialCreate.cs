using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaRoom.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "senha",
                table: "TB_Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "senha",
                table: "TB_Usuarios",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
