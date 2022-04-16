using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerse_API.Migrations
{
    public partial class EditUserTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Roles_roleId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_roleId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "roleId",
                table: "users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "roleId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_users_roleId",
                table: "users",
                column: "roleId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Roles_roleId",
                table: "users",
                column: "roleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
