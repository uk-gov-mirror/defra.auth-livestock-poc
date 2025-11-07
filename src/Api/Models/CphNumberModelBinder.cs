// <copyright file="CphNumberModelBinder.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Models;

using System.Threading.Tasks;
using Microsoft;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class CphNumberModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        Requires.NotNull(bindingContext);

        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrWhiteSpace(value))
        {
            return Task.CompletedTask;
        }

        try
        {
            var cphNumber = new CphNumber(value);
            bindingContext.Result = ModelBindingResult.Success(cphNumber);
        }
#pragma warning disable CA1031
        catch (System.Exception ex)
#pragma warning restore CA1031
        {
            bindingContext.ModelState.TryAddModelError(
                modelName,
                $"Invalid CPH format: {ex.Message}");
        }

        return Task.CompletedTask;
    }
}
