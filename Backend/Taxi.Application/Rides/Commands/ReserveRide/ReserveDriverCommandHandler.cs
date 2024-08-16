using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Api;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides;

namespace Taxi.Application.Rides.Commands.ReserveRide
{
    internal sealed class ReserveDriverCommandHandler : ICommandHandler<ReserveDriverCommand, Ride>
    {
        private readonly IRideRepository _rideRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiService _apiService;

        public ReserveDriverCommandHandler(
            IRideRepository rideRepository,
            IUnitOfWork unitOfWork,
            IApiService apiService)
        {
            _rideRepository = rideRepository;
            _unitOfWork = unitOfWork;
            _apiService = apiService;
        }

        public async Task<Result<Ride>> Handle(ReserveDriverCommand request, CancellationToken cancellationToken)
        {
            var ride = await _rideRepository.GetByIdAsync(request.RideId);

            if (ride == null)
            {
                return Result.Failure<Ride>(RideErrors.NotFound);
            }



            try
            {
                var waitingTime = _apiService.PredictWaitingTime(ride.StartAddress.Value, ride.EndAddress.Value);

                ride.WaitingTime = new WaitingTime(waitingTime);
            }
            catch
            {
                return Result.Failure<Ride>(new Error("ApiService.Error", "Too many requests"));
            }

            //ride = Ride.Reserve(ride);

            //_rideRepository.Add(ride);
            _rideRepository.Update(ride);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(ride);
        }
    }
}
