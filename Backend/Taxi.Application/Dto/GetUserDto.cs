using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Users;

namespace Taxi.Application.Dto;

public class GetUserDto
{

    public Username Username { get; set; } 
    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; } 
    public Password Password { get; set; } 
    public Address Address { get; set; } 
    public Birthday Birthday { get; set; } 
    public UserType UserType { get; set; } 
    public Email Email { get; set; } 
    public IFormFile? File { get; set; }
}

