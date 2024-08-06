using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto;

namespace Taxi.Application.Users.GetUser
{
    public record GetUserQuery(
        Guid userId) : IQuery<GetUserDto>;
    
}
