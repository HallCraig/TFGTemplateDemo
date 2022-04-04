// <copyright file="PostMigration.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the PostMigration class.

namespace Tfg.TFGTemplateDemo.Data.Migrations
{
    #region 

    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;
    using System.IO;
    using System.Linq;
    using Tfg.EntityFramework;

    #endregion

    [DbContext(typeof(DatabaseContext))]
    [Migration("9999999999999_PostMigration")]
    public class PostMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.KeepAliveCustomMigration(migrationBuilder.GetMigrationId(typeof(PostMigration)));

            foreach (var command in Directory.EnumerateFiles($"{AppDomain.CurrentDomain.BaseDirectory}//Scripts", "*POST.sql")
                .OrderBy(file => file).SelectMany(path => EntityFrameworkHelper.ParseFileAndSplitLines(path, "GO")))
            {
                migrationBuilder.Sql(command);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.KeepAliveCustomMigration(migrationBuilder.GetMigrationId(typeof(PostMigration)));
        }
    }
}
