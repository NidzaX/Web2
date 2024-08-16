using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Api;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides;
using Taxi.Domain.Users;

namespace Taxi.Application.Rides.Commands.CreateRide
{
    internal sealed class CreateRideCommandHandler : ICommandHandler<CreateRideCommand, Ride>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IRideRepository _rideRepository;
        private readonly IApiService _apiService;

        public CreateRideCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IRideRepository rideRepository,
            IApiService apiService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _rideRepository = rideRepository;
            _apiService = apiService;
        }

        public async Task<Result<Ride>> Handle(CreateRideCommand request, CancellationToken cancellationToken)
        {

            var ride =
                 Ride.Create(
                     request.userId,
                     request.driverId,
                     request.startAddress,
                     request.endAddress);

            try
            {
                var calculatedPrice = _apiService.CalculatePrice(ride.StartAddress.Value, ride.EndAddress.Value);
                var pickUpTime = _apiService.EstimatePickupTime();

                ride.Price = new Price(calculatedPrice);
                ride.PredictedTime = new PredictedTime(pickUpTime);
            }
            catch
            {
                return Result.Failure<Ride>(new Error("ApiService.Error", "Too many requests"));
            }


            _rideRepository.Add(ride);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(ride);
        }
    }
}