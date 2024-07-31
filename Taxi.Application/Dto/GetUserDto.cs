using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Application.Dto;

public class GetUserDto
{

    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
   public DateTime Birthday { get; set; } 
    public string UserType { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

