using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addNullableTypeInBase : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_ProjectManagers_BuildingId",
                table: "ProjectManagers");

            migrationBuilder.DropIndex(
                name: "IX_Engineers_BuildingId",
                table: "Engineers");

            migrationBuilder.DropIndex(
                name: "IX_ChiefEngineers_BuildingId",
                table: "ChiefEngineers");

            migrationBuilder.AddColumn<int>(
                name: "ChiefEngineerId",
                table: "Buildings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectManagerId",
                table: "Buildings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BuildingEngineer",
                columns: table => new
                {
                    BuildingId = table.Column<int>(type: "integer", nullable: false),
                    EngineerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingEngineer", x => new { x.BuildingId, x.EngineerId });
                    table.ForeignKey(
                        name: "FK_BuildingEngineer_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingEngineer_Engineers_EngineerId",
                        column: x => x.EngineerId,
                        principalTable: "Engineers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ChiefEngineerId",
                table: "Buildings",
                column: "ChiefEngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ProjectManagerId",
                table: "Buildings",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingEngineer_EngineerId",
                table: "BuildingEngineer",
                column: "EngineerId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ChiefEngineers_ChiefEngineerId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ProjectManagers_ProjectManagerId",
                table: "Buildings");

            migrationBuilder.DropTable(
                name: "BuildingEngineer");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_ChiefEngineerId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_ProjectManagerId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "ChiefEngineerId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "Buildings");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManagers_BuildingId",
                table: "ProjectManagers",
                column: "BuildingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Engineers_BuildingId",
                table: "Engineers",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiefEngineers_BuildingId",
                table: "ChiefEngineers",
                column: "BuildingId",
                unique: true);

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
