using Taxi.Application.Abstractions.Messaging;

namespace Taxi.Application.Users.LogInUser
{
    public record LoginUserCommand(
        string Email,
        string Password)
        : ICommand<AccessTokenResponse>;

}
