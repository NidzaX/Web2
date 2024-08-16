using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto.Commands;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides;

namespace Taxi.Application.Rides.Commands.ReserveRideByDriver
{
    internal sealed class ReserveRideByDriverCommandHandler
        : ICommandHandler<ReserveRideByDriverCommand, Ride>
    {
        private readonly IRideRepository _rideRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReserveRideByDriverCommandHandler(
            IRideRepository rideRepository,
            IUnitOfWork unitOfWork)
        {
            _rideRepository = rideRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Ride>> Handle(ReserveRideByDriverCommand request, CancellationToken cancellationToken)
        {
            var ride = await _rideRepository.GetAvailableRideByIdAsync(request.RideId);

            if (ride == null)
            {
                return Result.Failure<Ride>(new Error("Ride.NotFoundOrAlreadyReserved", "The ride was not found or is already reserved."));
            }

            ride.AssignDriver(request.DriverId);

            _rideRepository.Update(ride);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(ride);
        }
    }
}
