using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Domain.Rides;

namespace Taxi.Application.Dto
{
    public record CreateRideDto(
         Guid userId,
         Guid? driverId,
         StartAddress startAddress,
         EndAddress endAddress);

}
