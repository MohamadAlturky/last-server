using System.Data;

namespace SharedKernal.Repositories;

public interface IUnitOfWork
{
    void SaveChanges();
    Task SaveChangesAsync();
	void EndTransaction();
	IDbTransaction BeginTransaction();
}