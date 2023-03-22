using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBankIdentity.Migrations
{
  /// <inheritdoc />
  public partial class AddUserToDataBase : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
            Email = table.Column<string>(type: "nvarchar(200)", nullable: false),
            Password = table.Column<string>(type: "nvarchar(500)", nullable: false),
            CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            Active = table.Column<bool>(type: "bit", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Users", x => x.Id);
          });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Users");
    }
  }
}
