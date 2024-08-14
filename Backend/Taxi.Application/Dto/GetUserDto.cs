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
    public string Username { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; } 
    public string Password { get; set; } 
    public string Address { get; set; } 
    public DateTime? Birthday { get; set; } 
    public string UserType { get; set; } 
    public string Email { get; set; } 
    public byte[] File { get; set; } 
}


