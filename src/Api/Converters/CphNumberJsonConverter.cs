// <copyright file="CphNumberJsonConverter.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Livestock.Auth.Models;
using Microsoft;

public class CphNumberJsonConverter
    : JsonConverter<CphNumber>
{
    public override CphNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var tmp = reader.GetString();
        return new CphNumber(tmp);
    }

    public override void Write(Utf8JsonWriter writer, CphNumber value, JsonSerializerOptions options)
    {
        Requires.NotNull(writer);
        Requires.NotNull(value);

        writer.WriteStringValue(value.ToString());
    }
}
