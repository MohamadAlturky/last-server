using Application.ExecutorProvider;
using Application.Loggers.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Loggers.Implementations;
internal class QueryLogger<TQuery> : IQueryLogger<TQuery>
{

	private readonly IExecutorIdentityProvider _executorIdentityProvider;
	private readonly ILogger<QueryLogger<TQuery>> _logger;

	public QueryLogger(IExecutorIdentityProvider executorIdentityProvider,
		ILogger<QueryLogger<TQuery>> logger)
	{
		_executorIdentityProvider = executorIdentityProvider;
		_logger = logger;
	}

	public void LogAfter(TQuery query)
	{
		_logger.LogError("Query Completed {@RequestName},{@DateTimeUTC},{@Executor}",
			typeof(TQuery).Name,
			DateTime.UtcNow,
			"executor");
	}

	public void LogBefore(TQuery query)
	{
		//string executor = _executorIdentityProvider.GetExecutor();
		_logger.LogError("Query Started {@RequestName},{@DateTimeUTC},{@Executor},{@RequestValue}",
			typeof(TQuery).Name,
			DateTime.UtcNow,
			"executor",
			query.ToString());
	}
}
