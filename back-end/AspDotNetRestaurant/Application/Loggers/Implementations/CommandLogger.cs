using Application.ExecutorProvider;
using Application.Loggers.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Loggers.Implementations;
internal class CommandLogger<TCommand> : ICommandLogger<TCommand>
{
	private readonly IExecutorIdentityProvider _executorIdentityProvider;
	private readonly ILogger<CommandLogger<TCommand>> _logger;

	public CommandLogger(IExecutorIdentityProvider executorIdentityProvider, 
		ILogger<CommandLogger<TCommand>> logger)
	{
		_executorIdentityProvider = executorIdentityProvider;
		_logger = logger;
	}


	public void LogAfter(TCommand command)
	{
		_logger.LogError("Command Completed {@RequestName},{@DateTimeUTC},{@Executor}",
			typeof(TCommand).Name,
			DateTime.UtcNow,
			"executor");
	}

	public void LogBefore(TCommand command)
	{
		//string executor = _executorIdentityProvider.GetExecutor();

		_logger.LogError("Command Started {@RequestName},{@DateTimeUTC},{@Executor},{@RequestValue}",
			typeof(TCommand).Name,
			DateTime.UtcNow,
			"executor",
			command.ToString());
	}
}
