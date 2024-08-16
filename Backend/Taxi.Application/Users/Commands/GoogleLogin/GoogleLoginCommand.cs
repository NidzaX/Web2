using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Users.Shared;


namespace Taxi.Application.Users.Commands.GoogleLogin
{
    public record GoogleLoginCommand(
        string Email,
        string Token) : ICommand<AccessTokenResponse>;

}
