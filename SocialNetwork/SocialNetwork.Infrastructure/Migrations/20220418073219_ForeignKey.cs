using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.Infrastructure.Migrations
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Subscription_SubscriberId",
                table: "Subscription",
                column: "SubscriberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Users_SubscriberId",
                table: "Subscription",
                column: "SubscriberId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Users_SubscriberId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_SubscriberId",
                table: "Subscription");
        }
    }
}
