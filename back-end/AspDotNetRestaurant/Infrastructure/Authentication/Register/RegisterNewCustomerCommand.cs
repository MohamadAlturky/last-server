using Infrastructure.Authentication.Models;
using SharedKernal.CQRS.Commands;

namespace Infrastructure.Authentication.Register;
public sealed record RegisterNewCustomerCommand(User user, string password) : ICommand;

