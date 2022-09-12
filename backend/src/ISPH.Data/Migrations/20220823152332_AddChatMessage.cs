using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISPH.Data.Migrations
{
    public partial class AddChatMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(name: "FK_Chats_Employers_EmployerId", column: x => x.EmployerId, principalTable: "Employers",
                        principalColumn: "Id", onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(name: "FK_Chats_Students_StudentId", column: x => x.StudentId, principalTable: "Students",
                        principalColumn: "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeftAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(name: "FK_Messages_Chats_ChatId", column: x => x.ChatId, principalTable: "Chats",
                        principalColumn: "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(name: "IX_Chats_EmployerId", table: "Chats", column: "EmployerId");
            migrationBuilder.CreateIndex(name: "IX_Chats_StudentId", table: "Chats", column: "StudentId");
            migrationBuilder.CreateIndex(name: "IX_Messages_ChatId", table: "Messages", column: "ChatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Messages");
            migrationBuilder.DropTable(name: "Chats");
        }
    }
}
