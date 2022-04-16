using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerse_API.Migrations
{
    public partial class EditDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "users",
                newName: "Username");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
