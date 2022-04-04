// <copyright file="ProductEntityConfiguration.cs" company="TFG">
//     Copyright © TFG.
// </copyright>

// Purpose:         Defines the ProductEntityConfiguration class.

#region Usings

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

#endregion

////namespace Tfg.TFGTemplateDemo
////{
////class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
////{
////    public void Configure(EntityTypeBuilder<Product> builder)
////    {
////        builder.HasKey("Id")
////           .IsClustered(true);

////        builder.Property(x => x.DateCreated)
////           .IsRequired();

////        builder.Property(x => x.DateModified)
////           .IsRequired();


////        builder.Property(p => p.Name)
////            .IsRequired(true)
////            .HasMaxLength(100);

////        builder.Property(p => p.Code)
////            .IsRequired(true)
////            .HasMaxLength(10);

////        builder.Property(p => p.IsRetired)
////            .IsRequired(true)
////            .HasDefaultValue(false);

////        builder.Property(p => p.UserCreated)
////            .IsRequired(false)
////            .HasMaxLength(20);

////        builder.Property(p => p.UserModified)
////            .IsRequired(true)
////            .HasMaxLength(20);

////        //builder.Ignore(p => p.Version);
////    }
////}
////}
