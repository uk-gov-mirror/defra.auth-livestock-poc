// <copyright file="Cph.Get.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Controllers;

using System.Threading;
using System.Threading.Tasks;
using Livestock.Auth.Context;
using Livestock.Auth.Models;
using Microsoft;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// The cph endpoint.
/// </summary>
[ApiController]
[Route("/cph")]
[SwaggerTag("Manage the County Parish Holding (CPH) numbers")]
public partial class Cph(
    IDbContextFactory<AuthStoreContext> contextFactory,
    ILogger<Cph> logger)
    : ControllerBase
{
    [HttpGet("{cph}")]
    [SwaggerOperation(
        Summary = "Get CPH information",
        Description = "Retrieves information for a specific County Parish Holding number",
        OperationId = "GetCph",
        Tags = new[] { "CPH" })]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved CPH information", typeof(Livestock.Auth.Context.DataModel.Cph))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid CPH format", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "CPH not found", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error", typeof(ProblemDetails))]
    public async Task<IResult> GetAsync(
        [FromRoute, SwaggerParameter("The CPH number to retrieve", Required = true)] CphNumber cph,
        CancellationToken cancellationToken)
    {
        Requires.NotNull(cph);

        using var db = contextFactory.CreateDbContext();

        var result = await db
            .Cphs
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.Reference == cph, cancellationToken)
            .ConfigureAwait(false);

        if (result is null)
        {
            return Results.NotFound();
        }

        return Results.Json(cph);
    }
}
