using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Rides;

namespace Taxi.Application.Rides.CreateRide
{
    public record CreateRideCommand(
         Guid userId,
         Guid driverId,
         StartAddress startAddress,
         EndAddress endAddress,
         PricingService PricingService,
         DateTime createdOnUtc) : ICommand<Guid>;


}
