using MediatR;
using SharedKernal.Utilities.Result;

namespace SharedKernal.CQRS.LogableCommand;
public interface LogableCommandHandler<TCommand>:IRequestHandler<TCommand,Result>
	where TCommand: ILogableCommand<Result>
{ }