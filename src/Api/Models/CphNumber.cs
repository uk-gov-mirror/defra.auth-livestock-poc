// <copyright file="CphNumber.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Models;

using System;
using System.Text.Json.Serialization;
using System.Web;
using Livestock.Auth.Converters;
using Microsoft;
using Microsoft.EntityFrameworkCore;

[JsonConverter(typeof(CphNumberJsonConverter))]
[Keyless]
public class CphNumber
{
    public CphNumber()
    {
    }

    public CphNumber(string source)
    {
        ParseCph(source);
    }

    [JsonIgnore]
    public string County { get; private set; } = string.Empty;

    [JsonIgnore]
    public string Parish { get; private set; } = string.Empty;

    [JsonIgnore]
    public string Holding { get; private set; } = string.Empty;

    [JsonIgnore]
    public string? Number
    {
        get
        {
            return $"{County}/{Parish}/{Holding}";
        }

        set
        {
            Requires.NotNullOrWhiteSpace(value!);
            ParseCph(value);
        }
    }

    public override string ToString()
    {
        return Number ?? string.Empty;
    }

    private void ParseCph(string source)
    {
        Requires.NotNullOrWhiteSpace(source);

        var tmp = HttpUtility.UrlDecode(source);
        var parts = tmp.Split('/');

        if (parts.Length != 3)
        {
            throw new FormatException("Cph number must be in the format county/parish/holding");
        }

        County = parts[0];
        Parish = parts[1];
        Holding = parts[2];
    }
}
