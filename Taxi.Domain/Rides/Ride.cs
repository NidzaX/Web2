using Taxi.Domain.Abstractions;

namespace Taxi.Domain.Rides
{
    public sealed class Ride : Entity
    {
        public Ride(Guid id,
                    decimal price,
                    string startAddress,
                    string endAddress) 
            : base(id)
        {
            Price = price;
            StartAddress = startAddress;
            EndAddress = endAddress;
        }
    
        private Ride()
        {

        }

        public decimal Price { get; private set; }

        public string StartAddress { get; private set; }
        
        public string EndAddress { get; private set;}
    }




    
}
