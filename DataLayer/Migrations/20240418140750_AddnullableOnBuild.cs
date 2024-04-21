using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddnullableOnBuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ChiefEngineers_ChiefEngineerId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ProjectManagers_ProjectManagerId",
                table: "Buildings");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManagerId",
                table: "Buildings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ChiefEngineerId",
                table: "Buildings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ChiefEngineers_ChiefEngineerId",
                table: "Buildings",
                column: "ChiefEngineerId",
                principalTable: "ChiefEngineers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ProjectManagers_ProjectManagerId",
                table: "Buildings",
                column: "ProjectManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ChiefEngineers_ChiefEngineerId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ProjectManagers_ProjectManagerId",
                table: "Buildings");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectManagerId",
                table: "Buildings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChiefEngineerId",
                table: "Buildings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ChiefEngineers_ChiefEngineerId",
                table: "Buildings",
                column: "ChiefEngineerId",
                principalTable: "ChiefEngineers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ProjectManagers_ProjectManagerId",
                table: "Buildings",
                column: "ProjectManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
