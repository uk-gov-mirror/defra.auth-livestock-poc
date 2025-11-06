// <copyright file="Environment.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Config;

using Microsoft;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

public static class Environment
{
    public static bool IsDevMode(this WebApplicationBuilder builder)
    {
        Requires.NotNull(builder);

        return !builder.Environment.IsProduction();
    }
}
