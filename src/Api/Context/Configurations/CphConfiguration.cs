// <copyright file="CphConfiguration.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Context.Configurations;

using Livestock.Auth.Context.DataModel;
using Livestock.Auth.Models;
using Microsoft;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// The Cph table context details.
/// </summary>
public class CphConfiguration
    : IEntityTypeConfiguration<Cph>
{
    public void Configure(EntityTypeBuilder<Cph> builder)
    {
        Requires.NotNull(builder);

        builder
            .HasKey(e => e.CphId)
            .HasName("pch_pk");

        builder.Property(e => e.CphId)
            .HasDefaultValueSql("gen_random_uuid()");

        builder
            .Property(e => e.Reference)
            .HasConversion(
                v => v.Number ?? string.Empty,
                v => new CphNumber(v));

        builder
            .Property(e => e.CreatedDatetime)
            .HasDefaultValueSql("now()");

        builder.Property(e => e.IsActive)
            .HasDefaultValue(true);
    }
}
