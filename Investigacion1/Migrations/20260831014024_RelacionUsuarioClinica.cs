using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Investigacion1.Migrations
{
    /// <inheritdoc />
    public partial class RelacionUsuarioClinica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pacientes_Email",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Pacientes");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaNacimiento",
                table: "Pacientes",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Pacientes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Dermatologos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_UsuarioId",
                table: "Pacientes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dermatologos_UsuarioId",
                table: "Dermatologos",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dermatologos_Usuarios_UsuarioId",
                table: "Dermatologos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Usuarios_UsuarioId",
                table: "Pacientes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dermatologos_Usuarios_UsuarioId",
                table: "Dermatologos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Usuarios_UsuarioId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_UsuarioId",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_Dermatologos_UsuarioId",
                table: "Dermatologos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Dermatologos");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Usuarios",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaNacimiento",
                table: "Pacientes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Pacientes",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Email",
                table: "Pacientes",
                column: "Email",
                unique: true);
        }
    }
}
