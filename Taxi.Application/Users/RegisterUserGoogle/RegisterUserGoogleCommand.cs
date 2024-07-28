using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taxi.Application.Abstractions.Messaging;

namespace Taxi.Application.Users.RegisterUserGoogle
{
    public record RegisterUserGoogleCommand(
        string Username,
        string FirstName,
        string LastName,
        string Password,
        string Address,
        DateTime Birthday,
        string UserType,
        string Email,
        string File,
        bool Verified,
        string Token) : ICommand<Guid>;
}
