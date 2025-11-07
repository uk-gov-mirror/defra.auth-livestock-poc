// <copyright file="CphNumberModelBinderProvider.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Models;

using Microsoft;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class CphNumberModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        Requires.NotNull(context);

        return context.Metadata.ModelType == typeof(CphNumber) ?
            new CphNumberModelBinder() :
            null;
    }
}
