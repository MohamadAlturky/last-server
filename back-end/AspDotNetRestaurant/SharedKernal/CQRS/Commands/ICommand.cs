using MediatR;
using SharedKernal.Utilities.Result;

namespace SharedKernal.CQRS.Commands;
public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
