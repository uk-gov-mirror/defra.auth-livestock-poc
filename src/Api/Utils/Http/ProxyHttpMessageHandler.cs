// <copyright file="ProxyHttpMessageHandler.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Utils.Http;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using Microsoft;

public class ProxyHttpMessageHandler : HttpClientHandler
{
    [ExcludeFromCodeCoverage]
    public ProxyHttpMessageHandler()
    {
        var proxyUri = Environment.GetEnvironmentVariable("CDP_HTTPS_PROXY");
        var proxy = new WebProxy { BypassProxyOnLocal = true };
        if (proxyUri != null)
        {
            var uri = new UriBuilder(proxyUri);

            var credentials = GetCredentialsFromUri(uri);
            if (credentials != null)
            {
                proxy.Credentials = credentials;
            }

            // Remove credentials from URI to so they don't get logged.
            uri.UserName = string.Empty;
            uri.Password = string.Empty;
            proxy.Address = uri.Uri;
        }

        this.Proxy = proxy;
        this.UseProxy = proxyUri != null;
    }

    public static NetworkCredential? GetCredentialsFromUri(UriBuilder uri)
    {
        Requires.NotNull(uri);
        var username = uri.UserName;
        var password = uri.Password;
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        return new NetworkCredential(username, password);
    }
}
