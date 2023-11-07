using SharedKernal.Entities;

namespace SharedKernal.Repositories;
public interface IRepository<T> where T : IAggregateRoot
{
}
