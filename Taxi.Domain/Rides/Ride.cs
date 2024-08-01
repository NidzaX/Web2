using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides.Events;

namespace Taxi.Domain.Rides
{
    public sealed class Ride : Entity
    {
        private Ride(Guid id,
                    Guid userId,
                    Guid driverId,
                    double price,
                    double predictedTime,
                    string startAddress,
                    string endAddress,
                    RideStatus rideStatus,
                    DateTime createdOnUtc) 
            : base(id)
        {
            UserId = userId;
            DriverId = driverId;
            Price = price;
            PredictedTime = predictedTime;
            StartAddress = startAddress;
            EndAddress = endAddress;
            Status = rideStatus;
            CreatedOnUtc = createdOnUtc;
        }
    
        private Ride()
        {

        }

        public Guid UserId { get; private set; }

        public Guid DriverId { get; private set; }

        public double Price { get; private set; }
        public double PredictedTime { get; private set; }

        public string StartAddress { get; private set; }
        
        public string EndAddress { get; private set;}

        public DateTime CreatedOnUtc { get; private set; }

        public RideStatus Status { get; private set; }

        public DateTime? CancelledOnUtc { get; private set; }

        public DateTime? ConfirmedOnUtc { get; private set; }

        public DateTime? CompletedOnUtc { get; private set; }

        public DateTime? RejectedOnUtc { get; private set; }

        public static Ride Reserve(
            Guid userId,
            Guid driverId,
            string startAddress,
            string endAddress,
            PricingService pricingService,
            DateTime utcNow)
        {
            double price = pricingService.CalculatePrice(startAddress, endAddress);
            double predictedTime = pricingService.PredictWaitingTime(startAddress, endAddress);

            var ride = new Ride(
                Guid.NewGuid(),
                userId,
                driverId,
                price,
                predictedTime,
                startAddress,
                endAddress,
                RideStatus.Reserved,
                utcNow);

            ride.RaiseDomainEvent(new RideReservedDomainEvent(ride.DriverId));

            return ride;
        }

        public Result Confirm(DateTime utcNow)
        {
            if (Status != RideStatus.Reserved)
            {
                return Result.Failure(RideErrors.NotReserved);
            }

            Status = RideStatus.Confirmed;
            ConfirmedOnUtc = utcNow;

            RaiseDomainEvent(new RideConfirmedDomainEvent(DriverId));

            return Result.Success();
        }

        public Result Cancel(DateTime utcNow)
        {
            if(Status != RideStatus.Confirmed)
            {
                return Result.Failure(RideErrors.NotConfirmed);
            }

            Status = RideStatus.Cancelled;
            CancelledOnUtc = utcNow;

            RaiseDomainEvent(new RideCancelledDomainEvent(DriverId));

            return Result.Success();
        }

        public Result Complete(DateTime utcNow)
        {
            if(Status != RideStatus.Confirmed)
            {
                return Result.Failure(RideErrors.NotConfirmed);
            }

            Status = RideStatus.Completed;
            CompletedOnUtc = utcNow;

            RaiseDomainEvent(new RideCompletedDomainEvent(DriverId));

            return Result.Success();
        }

        public Result Reject(DateTime utcNow)
        {
            if(Status != RideStatus.Reserved)
            {
                return Result.Failure(RideErrors.NotReserved);
            }

            Status = RideStatus.Rejected;
            RejectedOnUtc = utcNow;

            RaiseDomainEvent(new RideRejectedDomainEvent(DriverId));

            return Result.Success();
        }
    }
}
