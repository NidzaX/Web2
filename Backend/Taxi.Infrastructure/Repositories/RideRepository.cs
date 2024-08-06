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
    }
}

