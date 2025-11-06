// <copyright file="Program.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth;

using System;
using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Livestock.Auth.Config;
using Livestock.Auth.Context;
using Livestock.Auth.Utils.Http;
using Livestock.Auth.Utils.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Serilog;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureBuilder(builder);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        SetupApplication(app);
        app.Run();
    }

    [ExcludeFromCodeCoverage]
    private static void ConfigureBuilder(WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();

        // builder.Services.AddDefaultAWSOptions(new AWSOptions { Region =   RegionEndpoint.EUWest2 });
        // Configure logging to use the CDP Platform standards.
        builder.Services.AddHttpContextAccessor();
        builder.Host.UseSerilog(CdpLogging.Configuration);

        // Default HTTP Client
        builder.Services
            .AddHttpClient("DefaultClient")
            .AddHeaderPropagation();

        // Proxy HTTP Client
        builder.Services.AddTransient<ProxyHttpMessageHandler>();
        builder.Services
            .AddHttpClient("proxy")
            .ConfigurePrimaryHttpMessageHandler<ProxyHttpMessageHandler>();

        // Propagate trace header.
        builder.Services.AddHeaderPropagation(options =>
        {
            var traceHeader = builder.Configuration.GetValue<string>("TraceHeader");
            if (!string.IsNullOrWhiteSpace(traceHeader))
            {
                options.Headers.Add(traceHeader);
            }
        });

        // Set up the Postgres connection.
        builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection("Mongo"));
        builder.Services
            .AddPooledDbContextFactory<AuthStoreContext>((sp, options) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("PostgresConnection");

                options
                    .UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>())
                    .UseNpgsql(
                        connectionString,
                        npgsqlOptions =>
                        {
                            npgsqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(10),
                                errorCodesToAdd: null);
                            npgsqlOptions.CommandTimeout(60);
                        })
                    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
            });

        builder.Services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });

        builder.Services.AddHealthChecks();
        builder.Services.AddValidatorsFromAssemblyContaining<MongoConfig>();
    }

    [ExcludeFromCodeCoverage]
    private static void SetupApplication(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseHeaderPropagation();
        app.UseRouting();
        app.MapHealthChecks("/health");
    }
}
