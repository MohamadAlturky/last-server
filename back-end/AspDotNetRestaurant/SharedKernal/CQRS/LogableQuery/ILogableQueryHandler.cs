using MediatR;
namespace SharedKernal.CQRS.LogableQuery;
public interface ILogableQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
	where TQuery : ILogableQuery<TResponse> { }
