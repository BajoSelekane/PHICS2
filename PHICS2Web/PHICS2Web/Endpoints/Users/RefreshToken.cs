using Application.Users.Login;
using Extensions;
using Infrastructure;
using MediatR;
using SharedLibrary.Shared;

namespace PHICS2Web.Endpoints.Users
{
    public class RefreshToken : IEndpoint
    {
        public record Request(string Token);
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/users/tokenrefresh", async (
          Request request,
          ISender sender,
          CancellationToken cancellationToken) =>
            {
                var command = new RefreshTokenUserCommand(request.Token);

                Result<string> result =
                    (Result<string>)await sender.Send(command, cancellationToken);

                return result.Match(
                    token => Results.Ok(new { access_token = token }),
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
}
    //RefreshTokenUserCommand
    //app.MapPost("/refresh-token", async (
    //      RefreshToken token,
    //      IUserAccount accountInterface,
    //      CancellationToken cancellationToken) =>
    //{
    //    if (token is null)
    //        return Results.BadRequest("Token is Empty");

    //    var result = await accountInterface.RefreshTokenAsync(token);
    //    return Results.Ok(result);
    //})
    //  .WithName("RefreshToken")
    //  .Produces(StatusCodes.Status200OK)
    //  .Produces(StatusCodes.Status400BadRequest);

    //return app;

    

