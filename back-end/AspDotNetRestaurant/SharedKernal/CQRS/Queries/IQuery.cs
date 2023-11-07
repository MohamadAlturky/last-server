using MediatR;
using SharedKernal.Utilities.Result;

namespace SharedKernal.CQRS.Queries;
public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }