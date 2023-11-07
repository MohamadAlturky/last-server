using MediatR;

namespace SharedKernal.CQRS.LogableQuery;

public interface ILogableQuery<T> : IRequest<T> { }
