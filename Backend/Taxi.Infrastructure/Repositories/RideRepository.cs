using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Rides;

namespace Taxi.Infrastructure.Repositories
{
    public sealed class RideRepository : Repository<Ride>, IRideRepository
    {

        public RideRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Ride>> GetAllRidesAsync()
        {
            return await DbContext
                .Set<Ride>()
                .ToListAsync();
        }

        public async Task<List<Ride>> GetAvailableRidesAsync()
        {
            return await DbContext
                .Set<Ride>()
                .Where(ride => ride.DriverId == null)
                .ToListAsync();
        }

        public async Task<Ride?> GetAvailableRideByIdAsync(Guid rideId)
        {
            return await DbContext
                .Set<Ride>()
                .FirstOrDefaultAsync(ride => ride.Id == rideId && ride.DriverId == null);
        }

        public async Task<List<Ride>> GetCompletedRides(Guid driverId)
        {
            return await DbContext
                .Set<Ride>()
                .Where(ride => ride.DriverId == driverId)
                .ToListAsync();
        }

        public async Task<List<Ride>> GetRidesByUserIdAsync(Guid userId)
        {
            return await DbContext
                .Set<Ride>()
                .Where(ride => ride.UserId == userId)
                .ToListAsync();
        }
    }
}