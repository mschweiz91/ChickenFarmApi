using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChickenFarmApi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddChickencs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EggLayingRecords",
                table: "EggLayingRecords");

            migrationBuilder.RenameTable(
                name: "EggLayingRecords",
                newName: "Chickens");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Chickens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Month",
                table: "Chickens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "EggCount",
                table: "Chickens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ChickenId",
                table: "Chickens",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Chickens",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Chickens",
                type: "TEXT",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ChickenId",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Chickens");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Chickens");

            migrationBuilder.RenameTable(
                name: "Chickens",
                newName: "EggLayingRecords");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "EggLayingRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Month",
                table: "EggLayingRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EggCount",
                table: "EggLayingRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EggLayingRecords",
                table: "EggLayingRecords",
                column: "Id");
        }
    }
}
