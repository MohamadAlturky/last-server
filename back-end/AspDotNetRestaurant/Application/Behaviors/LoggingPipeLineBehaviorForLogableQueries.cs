using Application.Loggers.Interfaces;
using Application.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using SharedKernal.CQRS.LogableQuery;
using SharedKernal.Utilities.Result;

namespace Application.Behaviors;
public class LoggingPipeLineBehaviorForLogableQueries<TRequest, TResponse>
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : ILogableQuery<TResponse>
	where TResponse : Result
{
	private readonly IQueryLogger<TRequest> _queryLogger;
	private readonly PipeLineSettings _settings;

	public LoggingPipeLineBehaviorForLogableQueries(IQueryLogger<TRequest> queryLogger, IOptionsMonitor<PipeLineSettings> options)
	{
		_queryLogger = queryLogger;
		_settings = options.CurrentValue;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		if (_settings.LogQueruesEnabled == false)
		{
			return await next();
		}

		_queryLogger.LogBefore(request);

		var result = await next();

		_queryLogger.LogAfter(request);

		return result;

	}
}
