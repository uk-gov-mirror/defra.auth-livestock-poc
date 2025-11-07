// <copyright file="EnvironmentTest.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Test.Config;

using Microsoft.AspNetCore.Builder;
using Environment = Livestock.Auth.Config.Environment;

public class EnvironmentTest
{
    [Fact]
    public void IsNotDevModeByDefault()
    {
        var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions());
        var isDev = Environment.IsDevMode(builder);
        Assert.False(isDev);
    }
}
