using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto;
using ICommand = Taxi.Application.Abstractions.Messaging.ICommand;

namespace Taxi.Application.Users.Commands.ChangeUserPassword
{
    public record ChangeUserPasswordCommand(
        string Email,
        string NewPassword,
        string OldPassword) : ICommand;

}
