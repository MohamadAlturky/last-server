using MediatR;
using SharedKernal.Utilities.Result;

namespace SharedKernal.CQRS.Queries;

public interface IQueryHandler<TQuery,TResponse>
	: IRequestHandler<TQuery, Result<TResponse>> 
		where TQuery : IQuery<TResponse> { }