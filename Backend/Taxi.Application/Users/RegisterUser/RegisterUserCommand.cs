using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taxi.Application.Abstractions.Messaging;

namespace Taxi.Application.Users.Commands
{
    public record RegisterUserCommand(
        string username,
        string firstName,
        string lastName,
        string password,
        string address,
        DateTime birthday,
        string userType,
        string email,
        IFormFile file) : ICommand<Guid>;
    
}
