using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides;

namespace Taxi.Application.Rides.GetCompletedRides
{
    internal sealed class GetCompletedRidesQueryHandler
        : IQueryHandler<GetCompletedRidesQuery, List<GetCompletedRidesDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRideRepository _rideRepository;

        public GetCompletedRidesQueryHandler(
            IMapper mapper,
            IRideRepository rideRepository)
        {
            _mapper = mapper;
            _rideRepository = rideRepository;
        }

        public async Task<Result<List<GetCompletedRidesDto>>> Handle(GetCompletedRidesQuery request, CancellationToken cancellationToken)
        {
            var rides = await _rideRepository.GetCompletedRides(request.driverId);

            if(rides == null || rides.Count == 0)
            {
                return Result.Failure<List<GetCompletedRidesDto>>(RideErrors.NotFound);
            }

            var ridesDto = _mapper.Map<List<GetCompletedRidesDto>>(rides);

            return Result.Success(ridesDto);
        }
    }
}

