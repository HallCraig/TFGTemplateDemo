using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tfg.TFGTemplateDemo.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Tfg");

            migrationBuilder.CreateTable(
                name: "AuditTransaction",
                schema: "Tfg",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: false),
                    UniqueId = table.Column<string>(maxLength: 2147483647, nullable: true),
                    TimeElapsed = table.Column<long>(nullable: false),
                    ServiceName = table.Column<string>(maxLength: 2147483647, nullable: true),
                    OperationName = table.Column<string>(maxLength: 2147483647, nullable: true),
                    OperationType = table.Column<string>(maxLength: 2147483647, nullable: true),
                    Host = table.Column<string>(maxLength: 2147483647, nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    Reference = table.Column<string>(maxLength: 2147483647, nullable: true),
                    UserName = table.Column<string>(maxLength: 2147483647, nullable: true),
                    LedgerType = table.Column<string>(maxLength: 2147483647, nullable: true),
                    Detail = table.Column<string>(maxLength: 2147483647, nullable: true),
                    ServerName = table.Column<string>(maxLength: 250, nullable: true),
                    IPAddress = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTransaction", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Tfg",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 4000, nullable: true),
                    Name = table.Column<string>(maxLength: 4000, nullable: true),
                    IsRetired = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 4000, nullable: true),
                    UserModified = table.Column<string>(maxLength: 4000, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditTransaction_DateCreated",
                schema: "Tfg",
                table: "AuditTransaction",
                column: "DateCreated")
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTransaction",
                schema: "Tfg");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Tfg");
        }
    }
}
