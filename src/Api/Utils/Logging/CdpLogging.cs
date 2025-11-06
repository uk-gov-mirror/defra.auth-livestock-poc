// <copyright file="CdpLogging.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Utils.Logging;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

public static class CdpLogging
{
    [ExcludeFromCodeCoverage]
    public static void Configuration(HostBuilderContext ctx, LoggerConfiguration config)
    {
        Requires.NotNull(ctx);
        Requires.NotNull(config);

        var httpAccessor = ctx.Configuration.Get<HttpContextAccessor>();
        var traceIdHeader = ctx.Configuration.GetValue<string>("TraceHeader");
        var serviceVersion = Environment.GetEnvironmentVariable("SERVICE_VERSION") ?? string.Empty;

        config
            .ReadFrom.Configuration(ctx.Configuration)
            /*.Enrich.WithEcsHttpContext(httpAccessor!)*/
            .Enrich.FromLogContext()
            .Enrich.WithProperty("service.version", serviceVersion);

        if (traceIdHeader != null)
        {
            config.Enrich.WithCorrelationId(traceIdHeader);
        }
    }
}
