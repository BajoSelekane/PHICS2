
using Login = Domain.Login;

namespace PHCISApp.Web.Endpoints.Authentication
{
    public static class AuthenticationEndpoints
    {
        public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
        {
            //var group = app.MapGroup("/api/authentication")
            //               .WithTags("Authentication");

            // REGISTER
            //group.MapPost("/register", async (
            //    Register user,
            //    IUserAccount accountInterface,
            //    CancellationToken cancellationToken) =>
            //{
            //    if (user is null)
            //        return Results.BadRequest("Model is created");

            //    var result = await accountInterface.CreateAsync(user);
            //    return Results.Ok(result);
            //})
            //.WithName("Register")
            //.Produces(StatusCodes.Status200OK)
            //.Produces(StatusCodes.Status400BadRequest);

            // LOGIN
            //group.MapPost("/login", async (
            //    Login user,
            //    IUserAccount accountInterface,
            //    CancellationToken cancellationToken) =>
            //{
            //    if (user is null)
            //        return Results.BadRequest("SignIn is Empty");

            //    var result = await accountInterface.SignInAsync(user);
            //    return Results.Ok(result);
            //})
            //.WithName("Login")
            //.Produces(StatusCodes.Status200OK)
            //.Produces(StatusCodes.Status400BadRequest);

            // REFRESH TOKEN
            //group.MapPost("/refresh-token", async (
            //    RefreshToken token,
            //    IUserAccount accountInterface,
            //    CancellationToken cancellationToken) =>
            //{
            //    if (token is null)
            //        return Results.BadRequest("Token is Empty");

            //    var result = await accountInterface.RefreshTokenAsync(token);
            //    return Results.Ok(result);
            //})
            //.WithName("RefreshToken")
            //.Produces(StatusCodes.Status200OK)
            //.Produces(StatusCodes.Status400BadRequest);

            return app;
        }
    }

}

