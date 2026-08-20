using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedMockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "ExpiryTime", "Password", "RefreshToken", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi", null, "Admin", "admin" },
                    { 2, "captain_a@gmail.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi", null, "Captain", "captain_a" },
                    { 3, "captain_b@gmail.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi", null, "Captain", "captain_b" },
                    { 4, "player_a@gmail.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi", null, "User", "player_a" },
                    { 5, "player_b@gmail.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi", null, "User", "player_b" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CaptainId", "LogoURL", "TeamName", "TournamentId" },
                values: new object[,]
                {
                    { 1, 2, "", "Team 1", null },
                    { 2, 3, "", "Team 2", null }
                });

            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "Id", "InGameName", "TeamId", "UserId" },
                values: new object[,]
                {
                    { 1, "captain_a", 1, 2 },
                    { 2, "player_a", 1, 4 },
                    { 3, "captain_b", 2, 3 },
                    { 4, "player_b", 2, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
