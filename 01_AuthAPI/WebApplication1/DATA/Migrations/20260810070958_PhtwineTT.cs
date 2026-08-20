using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class PhtwineTT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameTitle",
                table: "Tournaments");

            migrationBuilder.RenameColumn(
                name: "ToltalPCs",
                table: "Venues",
                newName: "TotalPCs");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Rewards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_TournamentId",
                table: "Rewards",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rewards_Tournaments_TournamentId",
                table: "Rewards",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rewards_Tournaments_TournamentId",
                table: "Rewards");

            migrationBuilder.DropIndex(
                name: "IX_Rewards_TournamentId",
                table: "Rewards");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Rewards");

            migrationBuilder.RenameColumn(
                name: "TotalPCs",
                table: "Venues",
                newName: "ToltalPCs");

            migrationBuilder.AddColumn<int>(
                name: "GameTitle",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TeamMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
