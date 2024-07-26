using Taxi.Domain.Abstractions;

namespace Taxi.Domain.Rides
{
    public sealed class Ride : Entity
    {
        public Ride(Guid id,
                    Guid userId,
                    Guid driverId,
                    decimal price,
                    string startAddress,
                    string endAddress) 
            : base(id)
        {
            UserId = userId;
            DriverId = driverId;
            Price = price;
            StartAddress = startAddress;
            EndAddress = endAddress;
        }
    
        private Ride()
        {

        }

        public Guid UserId { get; private set; }

        public Guid DriverId { get; private set; }

        public decimal Price { get; private set; }

        public string StartAddress { get; private set; }
        
        public string EndAddress { get; private set;}
    }




    
}
