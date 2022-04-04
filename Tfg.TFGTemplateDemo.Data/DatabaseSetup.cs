// <copyright file="DatabaseSetup.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the DatabaseSetup class.

// Date created:    02 08 2022

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;
    using System.Linq;
    using Tfg.DotNet;

    #endregion

    public class DatabaseSetup : DbContext
    {
        #region Variables

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseSetup"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DatabaseSetup(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseSetup"/> class.
        /// </summary>
        /// <param name="isDatabaseContext">if set to <c>true</c> [is database context].</param>
        public DatabaseSetup(bool isDatabaseContext) : this()
        {

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseSetup"/> class.
        /// </summary>
        public DatabaseSetup()
        {
        }

        #endregion

        #region Destructor

        #endregion

        #region Properties

        public DbSet<Product> Product { get; set; }

        public DbSet<AuditTransaction> AuditTransaction { get; set; }

        #endregion

        #region Methods

        #region Protected

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but before the model has been locked down and used to initialize the
        /// context. The default implementation of this method does nothing, but it can be overridden in a derived class such that the model can be further configured
        /// before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            modelBuilder.ApplyConfiguration(new AuditTransactionConfiguration());

            MapAssemblyToSchema(modelBuilder, "Tfg.TFGTemplateDemo.Data.Entities.dll", "Tfg");

            // this replaced the modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>() as in EF core it needs 
            // to set on each type
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                .SelectMany(t => t.GetProperties())
                                .Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                    property.SetMaxLength(4000);
            }

        }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IMigrationsSqlGenerator, DatabaseSqlGenerator>();
        }

        #endregion Protected

        #region Private

        /// <summary>
        /// Maps the assembly to schema.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="schemaName">Name of the schema.</param>
        void MapAssemblyToSchema(ModelBuilder modelBuilder, string assemblyName, string schemaName)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().First(a => String.Compare(a.ManifestModule.Name, assemblyName, true) == 0);
            foreach (Type entityType in assembly.GetTypes())
            {
                if (entityType.IsClass && (entityType.Name.IndexOf("base", StringComparison.OrdinalIgnoreCase) < 0))
                {
                    modelBuilder.Entity(entityType).ToTable(entityType.Name, schemaName);
                }
            }
        }

        #endregion

        #endregion
    }
}
