// <copyright file="User.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Context.DataModel;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// The User table model.
/// </summary>
[Table("Users", Schema = "defra_strategy_auth")]
public partial class User
{
    [Key]
    [Column("user_entra_id")]
    public Guid UserEntraId { get; set; }

    [MaxLength(256)]
    [Column("email_address")]
    public string EmailAddress { get; set; } = null!;

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_datetime")]
    public DateTime CreatedDatetime { get; set; }

    [Column("deleted_datetime")]
    public DateTime? DeletedDatetime { get; set; }

    public virtual ICollection<UserCphMapping> UserCphMappings { get; set; } = new List<UserCphMapping>();
}
