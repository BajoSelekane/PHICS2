using Application.Abstractions.Messaging;


namespace Application.Users.Login
{
    
    public sealed record RefreshTokenUserCommand(string Token) : ICommand<string>;
}
