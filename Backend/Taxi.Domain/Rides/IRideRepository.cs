using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Domain.Rides
{
    public interface IRideRepository
    {
        Task<Ride?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Ride>> GetRidesByUserIdAsync(Guid userId);
        Task<List<Ride>> GetAvailableRidesAsync();
        Task<List<Ride>> GetAllRidesAsync();
        Task<List<Ride>> GetCompletedRides(Guid driverId);
        Task<Ride> GetAvailableRideByIdAsync(Guid rideId);

        void Add(Ride rides);
        void Update(Ride rides);
    }
}
