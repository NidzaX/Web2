using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides;

namespace Taxi.Application.Rides.ReserveRide
{
    internal sealed class ReserveDriverCommandHandler : ICommandHandler<ReserveDriverCommand, Guid>
    {
        private readonly IRideRepository _rideRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReserveDriverCommandHandler(
            IRideRepository rideRepository,
            IUnitOfWork unitOfWork)
        {
            _rideRepository = rideRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ReserveDriverCommand request, CancellationToken cancellationToken)
        {
            var ride = await _rideRepository.GetByIdAsync(request.RideId);

            if (ride == null)
            {
                return Result.Failure<Guid>(RideErrors.NotFound);
            }

            ride = Ride.Reserve(ride, request.PricingService);

            _rideRepository.Add(ride);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(ride.Id);
        }
    }
}
