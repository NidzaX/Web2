using Taxi.Application.Abstractions.Messaging;

namespace Taxi.Application.Users.Commands
{
    public record LoginUserCommand(
        string Email,
        string Password) 
        : ICommand<AccessTokenResponse>;
   
}
