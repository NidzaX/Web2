using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Rides;

namespace Taxi.Application.Rides.ReserveRide
{
    public record ReserveDriverCommand(
        Guid RideId,
        PricingService PricingService) : ICommand<Guid>;
    
}
