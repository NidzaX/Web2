using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = Taxi.Application.Abstractions.Messaging.ICommand;


namespace Taxi.Application.Users.VerifyDriver;

public record VerifyDriverCommand(
    string Email,
    bool v) : ICommand;

