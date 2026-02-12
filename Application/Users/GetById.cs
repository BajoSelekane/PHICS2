using Application.Abstractions.Messaging;


namespace Application.Users
{
    public sealed record GetUserByIdQuery(Guid userId) : IQuery<string>;
}
