using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Abstractions;

namespace Taxi.Domain.Rides
{
    public static class RideErrors
    {
        public static readonly Error NotFount = new(
            "Ride.Error",
            "The ride with the specified identifier was not found");

        public static readonly Error AlredyStarted = new(
            "Ride.AlredyStarted",
            "The ride has alredy started");

    }
}
