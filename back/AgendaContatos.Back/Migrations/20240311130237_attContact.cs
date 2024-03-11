using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaContatos.Back.Migrations
{
    /// <inheritdoc />
    public partial class attContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "profileImg",
                table: "Contacts",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB");

            migrationBuilder.AddColumn<bool>(
                name: "isFavorite",
                table: "Contacts",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFavorite",
                table: "Contacts");

            migrationBuilder.AlterColumn<byte[]>(
                name: "profileImg",
                table: "Contacts",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);
        }
    }
}
