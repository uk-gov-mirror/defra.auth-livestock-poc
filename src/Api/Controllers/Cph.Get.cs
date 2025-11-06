// <copyright file="Cph.Get.cs" company="DEFRA">
// Copyright (c) Defra. All rights reserved.
// </copyright>

namespace Livestock.Auth.Controllers;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// The cph endpoint.
/// </summary>
[ApiController]
[Route("/cph")]
[SwaggerTag("Manage the County Parish Holding (CPH) numbers")]
public partial class Cph :
    ControllerBase
{
    [HttpGet("{cph}")]
    [SwaggerOperation(
        Summary = "Get CPH information",
        Description = "Retrieves information for a specific County Parish Holding number",
        OperationId = "GetCph",
        Tags = new[] { "CPH" })]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved CPH information", typeof(object))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid CPH format", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "CPH not found", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error", typeof(ProblemDetails))]
    public async Task<IResult> GetAsync(
        [FromRoute, SwaggerParameter("The CPH number to retrieve", Required = true)] string cph,
        CancellationToken cancellationToken)
    {
        await Task
            .Delay(10, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok();
    }
}
