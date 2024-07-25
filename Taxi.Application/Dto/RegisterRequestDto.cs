﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Application.Dto
{
    public record RegisterRequestDto(
        string username,
        string firstName,
        string lastName,
        string password,
        string address,
        DateTime birthday,
        string userType,
        string email,
        string file);
    
}