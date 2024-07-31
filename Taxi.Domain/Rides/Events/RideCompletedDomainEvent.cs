using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Abstractions;

namespace Taxi.Domain.Rides.Events
{
    public sealed record RideCompletedDomainEvent(Guid RideId) : IDomainEvent;
    
}
