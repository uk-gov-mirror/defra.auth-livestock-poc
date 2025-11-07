// <copyright file="ProxyHttpMessageHandlerTests.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Test.Utils.Http;

using System;
using FluentAssertions;
using Livestock.Auth.Utils.Http;

public class ProxyHttpMessageHandlerTests
{
    [Fact]
    public void ExtractsCredentialsFromUri()
    {
        var creds = ProxyHttpMessageHandler.GetCredentialsFromUri(
            new UriBuilder("http://username:password@www.example.com"));

        creds.Should().NotBeNull();
        Assert.NotNull(creds);
        creds.UserName.Should().Be("username");
        creds.Password.Should().Be("password");
    }

    [Fact]
    public void DoNotExtractCredentialsFromUriWithoutThem()
    {
        var creds = ProxyHttpMessageHandler.GetCredentialsFromUri(new UriBuilder("http://www.example.com"));
        creds.Should().BeNull();
    }
}
