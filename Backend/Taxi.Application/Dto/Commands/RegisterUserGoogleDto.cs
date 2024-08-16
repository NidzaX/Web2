using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Application.Dto.Commands
{
    public record RegisterUserGoogleDto(
        string Username,
        string FirstName,
        string LastName,
        string Password,
        string Address,
        DateTime Birthday,
        string UserType,
        string Email,
        IFormFile File,
        string Token);

}
