using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChickenFarmApi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddChickenNavigationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chickens_Chickens_ChickenId",
                table: "Chickens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chickens",
                table: "Chickens");

            migrationBuilder.DropIndex(
                name: "IX_Chickens_ChickenId",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "EggCount",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Chickens");

            migrationBuilder.AlterColumn<int>(
                name: "ChickenId",
                table: "Chickens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chickens",
                table: "Chickens",
                column: "ChickenId");

            migrationBuilder.CreateTable(
                name: "EggLayingRecords",
                columns: table => new
                {
                    RecordId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    EggCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ChickenId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EggLayingRecords", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_EggLayingRecords_Chickens_ChickenId",
                        column: x => x.ChickenId,
                        principalTable: "Chickens",
                        principalColumn: "ChickenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EggLayingRecords_ChickenId",
                table: "EggLayingRecords",
                column: "ChickenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EggLayingRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chickens",
                table: "Chickens");

            migrationBuilder.AlterColumn<int>(
                name: "ChickenId",
                table: "Chickens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Chickens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Chickens",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EggCount",
                table: "Chickens",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Chickens",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Chickens",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chickens",
                table: "Chickens",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chickens_ChickenId",
                table: "Chickens",
                column: "ChickenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chickens_Chickens_ChickenId",
                table: "Chickens",
                column: "ChickenId",
                principalTable: "Chickens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
