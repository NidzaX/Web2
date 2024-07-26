using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto;

namespace Taxi.Application.Users.ChangeUserPassword
{
    internal sealed record ChangeUserPasswordCommand(
        string Username,
        string NewPassword,
        string OldPassword) : ICommand<string>;
    
}
