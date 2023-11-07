using SharedKernal.Utilities.Errors;

namespace SharedKernal.Utilities.Result;
public static class ResultExtensions
{
	public static Result<T> Ensure<T>
		(this Result<T> result, Func<T, bool> predicate, Error error)
	{
		if (!result.IsSuccess)
		{
			return result;
		}
		else if (predicate(result.Value))
		{
			return result;
		}
		else
		{
			return Result.Failure<T>(error);
		}
	}

	public static Result<TOut> Map<T, TOut>
		(this Result<T> result, Func<T, TOut> mapper)
	{
		if (result.IsSuccess)
		{
			return Result.Create(mapper(result.Value));
		}
		else
		{
			return Result.Failure<TOut>(result.Error);
		}
	}


	public static Result<T> Tap<T>(this Result<T> result, Action<T> action)
	{
		if (result.IsSuccess)
		{
			action(result.Value);
		}
		return result;
	}

	public static async Task<Result<T>> Tap<T>(this Result<T> result, Func<Task> function)
	{
		if (result.IsSuccess)
		{
			await function();
		}
		return result;
	}
}