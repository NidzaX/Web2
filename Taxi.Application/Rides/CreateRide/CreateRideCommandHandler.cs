using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides;
using Taxi.Domain.Users;

namespace Taxi.Application.Rides.CreateRide
{
    internal sealed class CreateRideCommandHandler : ICommandHandler<CreateRideCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IRideRepository _rideRepository;

        public CreateRideCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IRideRepository rideRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _rideRepository = rideRepository;
        }

        public async Task<Result<Guid>> Handle(CreateRideCommand request, CancellationToken cancellationToken)
        {

            var (ride, price) =
                 Ride.Create(
                     request.userId,
                     request.driverId,
                     request.startAddress,
                     request.endAddress,
                     request.PricingService,
                     request.createdOnUtc);

            _rideRepository.Add(ride);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(ride.Id);
        }
    }
}