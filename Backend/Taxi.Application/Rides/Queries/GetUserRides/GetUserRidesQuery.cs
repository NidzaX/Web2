using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto.Queries;

namespace Taxi.Application.Rides.Queries.GetUserRides
{
    public record GetUserRidesQuery(
        Guid UserId) : IQuery<List<GetUserRideDto>>;

}
