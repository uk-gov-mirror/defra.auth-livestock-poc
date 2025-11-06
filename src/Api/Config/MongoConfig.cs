// <copyright file="MongoConfig.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Config;

public class MongoConfig
{
    public System.Uri DatabaseUri { get; init; } = default!;

    public string DatabaseName { get; init; } = default!;
}
