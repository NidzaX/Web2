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

        void Add(Ride rides);

    }
}
