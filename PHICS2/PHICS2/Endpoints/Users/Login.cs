
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Extensions;
using Infrastructure;
using Endpoints;
using Application.Users.Login;
using SharedLibrary.Shared;

namespace PHICS2.Endpoints.Users;


internal sealed class Login : IEndpoint
{
    public sealed record Request(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/login", async (
            Request request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<string> result =
                (Result<string>)await sender.Send(command, cancellationToken);

            return result.Match(
                Results.Ok,
                CustomResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Users)
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized);
        //.WithOpenApi();
    }
}

//internal sealed class Login : IEndpoint
//{
//    public sealed record Request(string Email, string Password);

//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapPost("/users/login", async (Request request, ISender sender, CancellationToken cancellationToken) =>
//        {
//            var command = new LoginUserCommand(request.Email, request.Password);

//            Result<string> result = await sender.Send(command, cancellationToken);

//            return result.Match(Results.Ok, CustomResults.Problem);
//        })
//        .WithTags(Tags.Users);
//    }
//}
