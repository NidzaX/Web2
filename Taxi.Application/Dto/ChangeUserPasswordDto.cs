﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Application.Dto
{
    public record ChangeUserPasswordDto(
        string Email,
        string NewPassword,
        string OldPassword);
    
}