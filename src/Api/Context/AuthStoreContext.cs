// <copyright file="AuthStoreContext.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Context;

using Livestock.Auth.Context.DataModel;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The Auth store context.
/// </summary>
public partial class AuthStoreContext : DbContext
{
    public AuthStoreContext()
    {
    }

    public AuthStoreContext(DbContextOptions<AuthStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cph> Cphs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCphMapping> UserCphMappings { get; set; }
}
