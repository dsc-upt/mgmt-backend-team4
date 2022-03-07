using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mgmt.Migrations
{
    public partial class UserProfileUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Users_userId",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "UserProfiles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfiles_userId",
                table: "UserProfiles",
                newName: "IX_UserProfiles_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Users_UserId",
                table: "UserProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Users_UserId",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserProfiles",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                newName: "IX_UserProfiles_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Users_userId",
                table: "UserProfiles",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
