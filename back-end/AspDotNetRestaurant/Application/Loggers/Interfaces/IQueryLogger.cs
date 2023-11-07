
using MediatR;

namespace Application.Loggers.Interfaces;
public interface IQueryLogger<TQuery>
{
	void LogAfter(TQuery query);
	void LogBefore(TQuery query);
}
