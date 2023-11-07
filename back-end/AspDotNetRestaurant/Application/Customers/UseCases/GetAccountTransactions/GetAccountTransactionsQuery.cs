using Domain.Customers.ReadModels;
using SharedKernal.CQRS.Queries;

namespace Application.Customers.UseCases.GetAccountTransactions;
public sealed record GetAccountTransactionsQuery(int serialNumber, int pageNumber)
	: IQuery<AccountTransactionsReadModel>;
