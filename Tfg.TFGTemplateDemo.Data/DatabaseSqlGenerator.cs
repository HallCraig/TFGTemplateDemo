// <copyright file="DatabaseSqlGenerator.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the DatabaseSqlGenerator class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Microsoft.EntityFrameworkCore.Migrations;
    using Tfg.EntityFramework;

    #endregion

    public class DatabaseSqlGenerator : SqlGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseSqlGenerator"/> class.
        /// </summary>
        /// <param name="dependencies">The dependencies.</param>
        /// <param name="migrationsAnnotations">The migrations annotations.</param>
        public DatabaseSqlGenerator(MigrationsSqlGeneratorDependencies dependencies, IMigrationsAnnotationProvider migrationsAnnotations) : base(dependencies, migrationsAnnotations)
        {
            defaultFileGroup = "Tfg";
            nonClusteredIndexFileGroup = $"{defaultFileGroup}_NCI";
            lobDataFileGroup = $"{defaultFileGroup}_LOB";

            // The primary key index clustered exception. Any table in this collection will have its isPrimaryKeyIndexClustered property inverted.
            primaryKeyIndexClusteredException = new System.Collections.ObjectModel.Collection<string>
            {
                "__EFMigrationsHistory"
            };
        }
    }
}