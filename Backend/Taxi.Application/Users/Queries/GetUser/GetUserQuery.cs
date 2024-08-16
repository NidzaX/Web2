using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto.Queries;

namespace Taxi.Application.Users.Queries.GetUser
{
    public record GetUserQuery(
        Guid userId) : IQuery<GetUserDto>;

}
