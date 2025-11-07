// <copyright file="GlobalSuppressions.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Usage",
    "CA2227: Collection properties should be read only",
    Justification = "Navigation properties need get/set.",
    Scope = "namespaceanddescendants",
    Target = "~N:Livestock.Auth.Context.DataModel")]
