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

namespace Taxi.Application.Rides.GetAllRides
{
    internal sealed class GetAllRidesQueryHandler
        : IQueryHandler<GetAllRidesQuery, List<GetAllRidesDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRideRepository _rideRepository;

        public GetAllRidesQueryHandler(IMapper mapper, IRideRepository rideRepository)
        {
            _mapper = mapper;
            _rideRepository = rideRepository;
        }

        public async Task<Result<List<GetAllRidesDto>>> Handle(GetAllRidesQuery request, CancellationToken cancellationToken)
        {
            var rides = await _rideRepository.GetAllRidesAsync();

            if(rides == null || rides.Count == 0)
            {
                return Result.Failure<List<GetAllRidesDto>>(RideErrors.NotFound);
            }

            var ridesDto = _mapper.Map<List<GetAllRidesDto>>(rides);

            return Result.Success(ridesDto);
        }
    }
}
