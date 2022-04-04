using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tfg.TFGTemplateDemo.Data.Migrations
{
    public partial class AddProductItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductItem",
                schema: "Tfg",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 4000, nullable: true),
                    Name = table.Column<string>(maxLength: 4000, nullable: true),
                    ItemNumber = table.Column<string>(maxLength: 4000, nullable: true),
                    IsRetired = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<string>(maxLength: 4000, nullable: true),
                    UserModified = table.Column<string>(maxLength: 4000, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductItem",
                schema: "Tfg");
        }
    }
}
