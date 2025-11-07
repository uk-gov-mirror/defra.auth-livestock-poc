// <copyright file="UserCphMapping.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Context.DataModel;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The User to CPH mapping table model.
/// </summary>
[Table("user_cph_mapping", Schema = "defra_strategy_auth")]
[PrimaryKey(nameof(UserEntraId), nameof(CphId), nameof(RoleType))]
public partial class UserCphMapping
{
    [Column("user_entra_id")]
    public Guid UserEntraId { get; set; }

    [Column("cph_id")]
    public Guid CphId { get; set; }

    [Column("role_type")]
    public int RoleType { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_datetime")]
    public DateTime CreatedDatetime { get; set; }

    [Column("deleted_datetime")]
    public DateTime? DeletedDatetime { get; set; }

    public virtual Cph Cph { get; set; } = null!;

    public virtual User UserEntra { get; set; } = null!;
}
