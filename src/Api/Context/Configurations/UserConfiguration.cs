// <copyright file="UserConfiguration.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Context.Configurations;

using Livestock.Auth.Context.DataModel;
using Microsoft;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        Requires.NotNull(builder);

        builder
            .HasIndex(
                e => new { e.EmailAddress, e.IsActive },
                "users_email_address_active_uindex")
            .IsUnique();

        builder
            .HasIndex(
                e => e.EmailAddress,
                "users_email_address_index");

        builder
            .Property(e => e.UserEntraId)
            .HasDefaultValueSql("gen_random_uuid()");

        builder
            .Property(e => e.CreatedDatetime)
            .HasDefaultValueSql("now()");
    }
}
