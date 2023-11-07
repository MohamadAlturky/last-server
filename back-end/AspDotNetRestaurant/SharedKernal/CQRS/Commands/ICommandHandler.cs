using MediatR;
using SharedKernal.Utilities.Result;

namespace SharedKernal.CQRS.Commands;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
            where TCommand : ICommand { }

public interface ICommandHandler<TCommand, TResponse>  : IRequestHandler<TCommand, Result<TResponse>>
            where TCommand : ICommand<TResponse> { }
