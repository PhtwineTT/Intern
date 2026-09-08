using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class StandardizeRoleValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$MIDQWAemvuAz0h/X67T7cuNoG8KoNeSvNm9rH0y.HVY9BTK/zClN2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$MIDQWAemvuAz0h/X67T7cuNoG8KoNeSvNm9rH0y.HVY9BTK/zClN2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$MIDQWAemvuAz0h/X67T7cuNoG8KoNeSvNm9rH0y.HVY9BTK/zClN2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$MIDQWAemvuAz0h/X67T7cuNoG8KoNeSvNm9rH0y.HVY9BTK/zClN2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$MIDQWAemvuAz0h/X67T7cuNoG8KoNeSvNm9rH0y.HVY9BTK/zClN2");
            migrationBuilder.Sql("UPDATE Users SET Role = ' Admin' WHERE LOWER(Role) = 'admin' ");
            migrationBuilder.Sql("UPDATE Users SET ROle = 'User' WHERE LOWER(Role) = 'user' ");
            migrationBuilder.Sql("UPDATE Users SET ROle = 'Captain' WHERE LOWER(Role) = 'captain'");
            migrationBuilder.Sql("UPDATE Users SET Role = 'Referee' WHERE LOWER(Role) = 'referee' ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$6FKddNxid0D2TigFrfzNXeimpVAnHmmiDWblYh3CkkNYsAt9MDSfi");
        }
    }
}
