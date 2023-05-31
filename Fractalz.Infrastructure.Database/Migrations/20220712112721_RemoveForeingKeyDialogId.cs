using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fractalz.Infrastructure.Database.Migrations
{
    public partial class RemoveForeingKeyDialogId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Messages_Dialogs_DialogId", "messages");
            migrationBuilder.DropIndex("IX_Messages_DialogId", "messages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey("FK_Messages_Dialogs_DialogId", "messages", "DialogId", "dialogs");
            migrationBuilder.AddUniqueConstraint("IX_Messages_DialogId", "messages", "DialogId");
        }
    }
}
