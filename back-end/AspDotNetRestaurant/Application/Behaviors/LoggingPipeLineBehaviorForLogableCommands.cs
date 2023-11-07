using MediatR;
using Application.Loggers.Interfaces;
using Application.Settings;
using Microsoft.Extensions.Options;
using SharedKernal.CQRS.LogableCommand;
using SharedKernal.Utilities.Result;

namespace Application.Behaviors;
public class LoggingPipeLineBehaviorForLogableCommands<TRequest, TResponse>
	: IPipelineBehavior<TRequest,TResponse>
	where TRequest : ILogableCommand<TResponse>
	where TResponse:Result
{
	private readonly ICommandLogger<TRequest> _commandLogger;
	private readonly PipeLineSettings _settings;

	public LoggingPipeLineBehaviorForLogableCommands(ICommandLogger<TRequest> commandLogger,
		IOptionsMonitor<PipeLineSettings> options)
	{
		_commandLogger = commandLogger;
		_settings = options.CurrentValue;
	}

	public async Task<TResponse> Handle(TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		if(_settings.LogCommandsEnabled == false)
		{
			return await next();
		}

		_commandLogger.LogBefore(request);

		var result = await next();
		
		_commandLogger.LogAfter(request);

		return result;
	}
}
