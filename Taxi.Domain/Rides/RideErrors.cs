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
        public static readonly Error NotFound = new(
            "Ride.Found",
            "The Ride with the specified identifier was not found");

        public static readonly Error NotReserved = new(
            "Ride.NotReserved",
            "The Ride is not pending");

        public static readonly Error NotConfirmed = new(
            "Ride.NotReserved",
            "The Ride is not confirmed");

        public static readonly Error AlredyExists = new(
           "Ride.AlredyExists",
           "The Ride alredy exists");
    }
}
