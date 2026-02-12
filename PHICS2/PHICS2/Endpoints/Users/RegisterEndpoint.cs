using Application.Users.Login;
using Endpoints;
using Extensions;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Shared;

namespace PHICS2.Endpoints.Users
{
    public class RegisterEndpoint : IEndpoint
    {
        public record Request(string PhoneNumber, string Email, string Password);
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/users/register", async (
          Request request,
          ISender sender,
          CancellationToken cancellationToken) =>
            {
                var command = new RegisterUserCommand(request.PhoneNumber,request.Email, request.Password);

                Result<string> result =
                    (Result<string>)await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
      .AllowAnonymous()
      .WithTags(Tags.Users)
      .Produces(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status401Unauthorized);
            //.WithOpenApi();
        }
    }
}
  