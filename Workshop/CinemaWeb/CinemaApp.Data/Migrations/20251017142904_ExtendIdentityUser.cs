using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UsersMovies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserTicket",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTicket", x => new { x.UserId, x.TicketId });
                    table.ForeignKey(
                        name: "FK_UserTicket_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTicket_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTicket_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersMovies_ApplicationUserId",
                table: "UsersMovies",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTicket_ApplicationUserId",
                table: "UserTicket",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTicket_TicketId",
                table: "UserTicket",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_AspNetUsers_ApplicationUserId",
                table: "UsersMovies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_AspNetUsers_ApplicationUserId",
                table: "UsersMovies");

            migrationBuilder.DropTable(
                name: "UserTicket");

            migrationBuilder.DropIndex(
                name: "IX_UsersMovies_ApplicationUserId",
                table: "UsersMovies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UsersMovies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");
        }
    }
}
