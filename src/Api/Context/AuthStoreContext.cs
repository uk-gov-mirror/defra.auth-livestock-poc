// <copyright file="AuthStoreContext.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Context;

using Microsoft.EntityFrameworkCore;

public class AuthStoreContext : DbContext
{
    public AuthStoreContext(DbContextOptions<AuthStoreContext> options)
        : base(options)
    {
    }

    // Add your DbSets here
    // public DbSet<YourEntity> YourEntities { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure your entity mappings here
    }
}
