using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Application.Dto
{
    public class GetAvailableRidesDto
    {
        public Guid RideId { get; set; }
        public Guid UserId { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public double Price { get; set; }
        public double PredictedTime { get; set; }
    }
}
