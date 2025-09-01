using MediatR;

namespace JWTAuthManager.Application.Common.Interfaces.Messaging.Queries;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}