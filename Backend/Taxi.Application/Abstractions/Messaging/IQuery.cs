using MediatR;
using Taxi.Domain.Abstractions;

namespace Taxi.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}
