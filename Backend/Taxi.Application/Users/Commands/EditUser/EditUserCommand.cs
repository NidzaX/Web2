using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taxi.Application.Abstractions.Messaging;
using ICommand = Taxi.Application.Abstractions.Messaging.ICommand;

namespace Taxi.Application.Users.Commands.EditUser
{
    public record EditUserCommand(
        string Username,
        string FirstName,
        string LastName,
        string Password,
        string Address,
        DateTime Birthday,
        string UserType,
        string Email,
        IFormFile File
        ) : ICommand;

}
