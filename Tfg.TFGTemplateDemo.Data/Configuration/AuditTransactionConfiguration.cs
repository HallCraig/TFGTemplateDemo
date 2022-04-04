// <copyright file="AuditTransactionConfiguration.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the AuditTransactionConfiguration class.

namespace Tfg.TFGTemplateDemo
{
    #region Usings

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    #endregion

    public class AuditTransactionConfiguration : IEntityTypeConfiguration<AuditTransaction>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<AuditTransaction> builder)
        {
            builder.HasKey("Id")
               .IsClustered(false);

            builder.HasIndex(p => new { p.DateCreated })
                .IsClustered(true)
                .IsUnique(false);

            builder.Property(x => x.UniqueId)
                .HasMaxLength(int.MaxValue);

            builder.Property(x => x.DateCreated)
               .IsRequired();

            builder.Property(x => x.DateModified)
               .IsRequired();

            builder.Property(x => x.TimeElapsed)
               .IsRequired();

            builder.Property(x => x.ServiceName)
                .HasMaxLength(int.MaxValue);

            builder.Property(x => x.OperationName)
                .HasMaxLength(int.MaxValue);

            builder.Property(x => x.OperationType)
                .HasMaxLength(int.MaxValue);

            builder.Property(x => x.Host)
               .HasMaxLength(int.MaxValue);

            builder.Property(x => x.StatusId)
               .IsRequired();

            builder.Property(x => x.Reference)
               .HasMaxLength(int.MaxValue);

            builder.Property(x => x.UserName)
              .HasMaxLength(int.MaxValue);

            builder.Property(x => x.LedgerType)
               .HasMaxLength(int.MaxValue);

            builder.Property(x => x.Detail)
               .HasMaxLength(int.MaxValue);

            builder.Property(x => x.ServerName)
                .HasMaxLength(250);

            builder.Property(x => x.IPAddress)
                .HasMaxLength(100);

            builder.Ignore(p => p.Name);
            builder.Ignore(p => p.Code);
            builder.Ignore(p => p.IsRetired);
            builder.Ignore(p => p.UserCreated);
            builder.Ignore(p => p.UserModified);
            builder.Ignore(p => p.Version);
        }
    }
}
