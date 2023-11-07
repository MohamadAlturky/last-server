using Infrastructure.Authentication.Models;
using SharedKernal.CQRS.Commands;

namespace Infrastructure.Authentication.LogIn;
public sealed record LogInCommand(LogInModel model) :ICommand<string>;  
