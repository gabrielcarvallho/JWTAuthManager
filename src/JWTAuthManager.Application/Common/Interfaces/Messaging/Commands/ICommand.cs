using MediatR;

namespace JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}