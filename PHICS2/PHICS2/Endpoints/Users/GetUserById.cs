//using Application.Users;
using Application.Users;
using Endpoints;
using Extensions;
using Infrastructure;
using MediatR;
using SharedLibrary.Shared;

namespace PHICS2.Endpoints.Users
{
    public class GetUserById : IEndpoint
    {
        public record Request(Guid userId);
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/users/{userId}", async (
           Request request,
           ISender sender,
           CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery(request.userId);

                Result<string> result =
                    (Result<string>)await sender.Send(query, cancellationToken);

                return result.Match( Results.Ok,CustomResults.Problem);
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
