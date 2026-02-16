using Application.Features.Patients.Commands;
using Application.Features.Patients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PHICS2Web.Endpoints.Patients
{
    public static class PatientEndpoints
    {
        public static void MapPatientEndpoints(this WebApplication app)
        {
            // Expose API endpoints under /api to avoid route collisions with Blazor pages
            var routeGroup = app.MapGroup("/api/patients");

            routeGroup.MapGet("/", async (ISender sender) =>
            {
                return await sender.Send(new GetAllPatientsQuery());
            });

            routeGroup.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
            {
                return await sender.Send(new GetPatientByIdQuery(id));
            });

            //routeGroup.MapPost("/", async (CreatePatientCommand cmd, ISender sender) =>
            //{
            //    return await sender.Send(cmd);
            //});

            //routeGroup.MapPut("/{id:guid}", async (Guid id,
            //   [FromBody] UpdatePatientCommand cmd,
            //   ISender sender) =>
            //{
            //    var command = cmd with { Id = id };

            //    var result = await sender.Send(command);

            //    return Results.Ok(result);
            //});

            routeGroup.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
            {
                await sender.Send(new DeletePatientCommand(id));
                return Results.NoContent();
            });
        }
    }

}
