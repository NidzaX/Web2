using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto.Queries;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides;

namespace Taxi.Application.Rides.Queries.GetAvailableRides
{
    internal sealed class GetAvailableRidesQueryHandler
        : IQueryHandler<GetAvailableRidesQuery, List<GetAvailableRidesDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRideRepository _rideRepository;

        public GetAvailableRidesQueryHandler(
            IMapper mapper,
            IRideRepository rideRepository)
        {
            _mapper = mapper;
            _rideRepository = rideRepository;
        }

        public async Task<Result<List<GetAvailableRidesDto>>> Handle(GetAvailableRidesQuery request, CancellationToken cancellationToken)
        {
            var rides = await _rideRepository.GetAvailableRidesAsync();

            if (rides == null || rides.Count == 0)
            {
                return Result.Failure<List<GetAvailableRidesDto>>(RideErrors.NotFound);
            }

            var ridesDto = _mapper.Map<List<GetAvailableRidesDto>>(rides);

            return Result.Success(ridesDto);
        }
    }
}

