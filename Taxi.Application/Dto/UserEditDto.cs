using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Taxi.Application.Dto
{
    public record UserEditDto(
        string Username,
        string FirstName,
        string LastName,
        string Password,
        string Address,
        DateTime Birthday,
        string UserType,
        string Email,
        string File,
        bool Verified);
    
}
