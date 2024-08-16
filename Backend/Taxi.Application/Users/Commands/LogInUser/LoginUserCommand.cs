using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Users.Shared;

namespace Taxi.Application.Users.Commands.LogInUser
{
    public record LoginUserCommand(
        string Email,
        string Password)
        : ICommand<AccessTokenResponse>;

}
