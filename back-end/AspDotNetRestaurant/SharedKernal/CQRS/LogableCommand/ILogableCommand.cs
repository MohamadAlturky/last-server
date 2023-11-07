using MediatR;

namespace SharedKernal.CQRS.LogableCommand;
public interface ILogableCommand<T> : IRequest<T> { }
