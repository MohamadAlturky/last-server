
namespace Application.Loggers.Interfaces;
public interface ICommandLogger<TCommand>
{
	void LogAfter(TCommand command);
	void LogBefore(TCommand command);

}
