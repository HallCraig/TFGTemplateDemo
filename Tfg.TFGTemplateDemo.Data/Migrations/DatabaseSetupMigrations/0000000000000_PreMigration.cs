// <copyright file="PreMigration.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the PreMigration class.

namespace Tfg.TFGTemplateDemo.Data.Migrations
{
    #region Usings

    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;
    using System.IO;
    using System.Linq;
    using Tfg.EntityFramework;

    #endregion

    [DbContext(typeof(DatabaseContext))]
    [Migration("0000000000000_PreMigration")]
    public class PreMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.KeepAliveCustomMigration(migrationBuilder.GetMigrationId(typeof(PreMigration)));

            foreach (var command in Directory.EnumerateFiles($"{AppDomain.CurrentDomain.BaseDirectory}//Scripts", "*PRE.sql")
                .OrderBy(file => file).SelectMany(path => EntityFrameworkHelper.ParseFileAndSplitLines(path, "GO")))
            {
                migrationBuilder.Sql(command, true);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.KeepAliveCustomMigration(migrationBuilder.GetMigrationId(typeof(PreMigration)));
        }
    }

}
