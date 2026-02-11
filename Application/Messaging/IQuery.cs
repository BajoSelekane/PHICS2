using MediatR;
using SharedLibrary.Shared;

namespace Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
