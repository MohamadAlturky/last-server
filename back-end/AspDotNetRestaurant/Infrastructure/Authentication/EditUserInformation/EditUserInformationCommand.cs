using Infrastructure.Authentication.Models;
using SharedKernal.CQRS.Commands;

namespace Infrastructure.Authentication.EditUserInformation;
public sealed record EditUserInformationCommand(User user) : ICommand;