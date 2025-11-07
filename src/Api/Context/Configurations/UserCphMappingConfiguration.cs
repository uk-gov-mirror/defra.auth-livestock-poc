// <copyright file="UserCphMappingConfiguration.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Context.Configurations;

using Livestock.Auth.Context.DataModel;
using Microsoft;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserCphMappingConfiguration
    : IEntityTypeConfiguration<UserCphMapping>
{
    public void Configure(EntityTypeBuilder<UserCphMapping> builder)
    {
        Requires.NotNull(builder);

        builder
            .HasIndex(
                e => e.CphId,
                "user_cph_mapping_cph_id_index");

        builder
            .HasIndex(
                e => e.UserEntraId,
                "user_cph_mapping_user_id_index");

        builder.Property(e => e.RoleType)
            .HasDefaultValue(1);

        builder.Property(e => e.CreatedDatetime)
            .HasDefaultValueSql("now()");

        builder.Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder
            .HasOne(d => d.Cph)
            .WithMany(p => p.UserCphMappings)
            .HasForeignKey(d => d.CphId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("user_cph_mapping_cphs_cph_id_fk");

        builder
            .HasOne(d => d.UserEntra)
            .WithMany(p => p.UserCphMappings)
            .HasForeignKey(d => d.UserEntraId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("user_cph_mapping_users_user_id_fk");
    }
}
