using Microsoft.EntityFrameworkCore.Migrations;

public partial class RemoveAuthAndGroupTables : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Drop Group-related tables
        migrationBuilder.DropTable(name: "GroupMessages");
        migrationBuilder.DropTable(name: "GroupMembers");
        migrationBuilder.DropTable(name: "Groups");

        // Drop Auth-related tables
        migrationBuilder.DropTable(name: "RefreshTokens");
        migrationBuilder.DropTable(name: "Auth");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // This is a destructive migration - we won't implement Down
        throw new NotImplementedException("This migration cannot be reverted");
    }
}