using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Domain.Rides
{
    public enum RideStatus
    {
        Cancelled = 0,
        Confirmed = 1,
        Completed = 2,
        Reserved = 3,
        Rejected = 4
    }
}
