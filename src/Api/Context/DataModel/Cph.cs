// <copyright file="Cph.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Context.DataModel;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Livestock.Auth.Converters;
using Livestock.Auth.Models;

/// <summary>
/// The Cph table model.
/// </summary>
[Table("cphs", Schema = "defra_strategy_auth")]
public partial class Cph
{
    [Column("pch_id")]
    public Guid CphId { get; set; }

    [Column("reference")]
    public CphNumber Reference { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_datetime")]
    public DateTime CreatedDatetime { get; set; }

    [Column("deleted_datetime")]
    public DateTime? DeletedDatetime { get; set; }

    public virtual ICollection<UserCphMapping> UserCphMappings { get; set; } = new List<UserCphMapping>();
}
