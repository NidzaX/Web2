﻿using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides.Events;

namespace Taxi.Domain.Rides
{
    public sealed class Ride : Entity
    {
        private Ride(Guid id,
                    Guid userId,
                    Guid? driverId,
                    Price price,
                    PredictedTime predictedTime,
                    WaitingTime? waitingTime,
                    StartAddress startAddress,
                    EndAddress endAddress,
                    RideStatus rideStatus,
                    DateTime createdOnUtc) 
            : base(id)
        {
            UserId = userId;
            DriverId = driverId;
            Price = price;
            PredictedTime = predictedTime;
            WaitingTime = waitingTime;
            StartAddress = startAddress;
            EndAddress = endAddress;
            Status = rideStatus;
            CreatedOnUtc = createdOnUtc;
        }
    
        private Ride()
        {

        }

        public Guid UserId { get; set; }

        public Guid? DriverId { get; set; }

        public Price Price { get; set; }

        public PredictedTime PredictedTime { get; set; }

        public WaitingTime? WaitingTime { get; set; }

        public StartAddress StartAddress { get; set; }
        
        public EndAddress EndAddress { get; set;}

        public DateTime CreatedOnUtc { get; set; }

        public RideStatus Status { get; set; }

        public DateTime? CancelledOnUtc { get; set; }

        public DateTime? ConfirmedOnUtc { get; set; }

        public DateTime? CompletedOnUtc { get; set; }

        public DateTime? RejectedOnUtc { get; set; }

        public static Ride Create(
           Guid userId,
           Guid? driverId,
           StartAddress startAddress,
           EndAddress endAddress)
        {
            //double price = new PricingService().CalculatePrice(startAddress.Value, endAddress.Value);
            //double pickUpTime = new PricingService().EstimatePickupTime();

            double price = 0.0;
            double pickUpTime = 0.0;

            var ride = new Ride(
                Guid.NewGuid(),
                userId,
                driverId,
                new Price(price),
                new PredictedTime(pickUpTime),
                null,
                startAddress,
                endAddress,
                RideStatus.Created,
                DateTime.UtcNow);

            //  ride.RaiseDomainEvent(new RideReservedDomainEvent(ride.DriverId));

            return ride;
        }


        public static Ride Reserve(
            Ride ride)
        {
            double waitingTime = new PricingService().PredictWaitingTime(ride.StartAddress.Value, ride.EndAddress.Value);

            ride.WaitingTime = new WaitingTime(waitingTime);

            //ride.RaiseDomainEvent(new RideReservedDomainEvent(ride.DriverId));

            return ride;
        }

        //public void UpdateWaitingTimeAndStatus(double waitingtime, RideStatus status)
        //{
        //    PredictedTime = new PredictedTime(waitingtime);
        //    Status = status;
        //}

        public void AssignDriver(Guid driverId)
        {
            if (DriverId != null)
            {
                throw new InvalidOperationException("Ride is already assigned to a driver.");
            }

            DriverId = driverId;
        }


        public Result Confirm(DateTime utcnow)
        {
            if (Status != RideStatus.Reserved)
            {
                return Result.Failure(RideErrors.NotReserved);
            }

            Status = RideStatus.Confirmed;
            ConfirmedOnUtc = utcnow;

            RaiseDomainEvent(new RideConfirmedDomainEvent(DriverId));

            return Result.Success();
        }

        public Result Cancel(DateTime utcNow)
        {
            if (Status != RideStatus.Confirmed)
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

        public Result Reject(DateTime utcnow)
        {
            if (Status != RideStatus.Reserved)
            {
                return Result.Failure(RideErrors.NotReserved);
            }

            Status = RideStatus.Rejected;
            RejectedOnUtc = utcnow;

            RaiseDomainEvent(new RideRejectedDomainEvent(DriverId));

            return Result.Success();
        }
    }
}
