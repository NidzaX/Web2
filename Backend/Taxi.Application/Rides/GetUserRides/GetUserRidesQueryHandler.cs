using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Review;
using Taxi.Domain.Rides;

namespace Taxi.Application.Rides.GetUserRides
{
    internal sealed class GetUserRidesQueryHandler
        : IQueryHandler<GetUserRidesQuery, List<GetUserRideDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRideRepository _rideRepository;

        public GetUserRidesQueryHandler(
            IMapper mapper,
            IRideRepository rideRepository)
        {
            _mapper = mapper;
            _rideRepository = rideRepository;
        }

        public async Task<Result<List<GetUserRideDto>>> Handle(GetUserRidesQuery request, CancellationToken cancellationToken)
        {
            var rides = await _rideRepository.GetRidesByUserIdAsync(request.UserId);
            if(rides == null || rides.Count == 0)
            {
                return Result.Failure<List<GetUserRideDto>>(RideErrors.NotFound);
            }

            var ridesDto = _mapper.Map<List<GetUserRideDto>>(rides);
            return Result.Success(ridesDto);
        }
    }
}

