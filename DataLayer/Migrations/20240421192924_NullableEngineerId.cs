using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class NullableEngineerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiefEngineers_Buildings_BuildingId",
                table: "ChiefEngineers");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineers_Buildings_BuildingId",
                table: "Engineers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManagers_Buildings_BuildingId",
                table: "ProjectManagers");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "ProjectManagers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Engineers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "ChiefEngineers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiefEngineers_Buildings_BuildingId",
                table: "ChiefEngineers",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Engineers_Buildings_BuildingId",
                table: "Engineers",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManagers_Buildings_BuildingId",
                table: "ProjectManagers",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiefEngineers_Buildings_BuildingId",
                table: "ChiefEngineers");

            migrationBuilder.DropForeignKey(
                name: "FK_Engineers_Buildings_BuildingId",
                table: "Engineers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManagers_Buildings_BuildingId",
                table: "ProjectManagers");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "ProjectManagers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Engineers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "ChiefEngineers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiefEngineers_Buildings_BuildingId",
                table: "ChiefEngineers",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Engineers_Buildings_BuildingId",
                table: "Engineers",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManagers_Buildings_BuildingId",
                table: "ProjectManagers",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
