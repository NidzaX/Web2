using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taxi.Application.Dto;
using Taxi.Application.Rides.CreateRide;
using Taxi.Application.Rides.ReserveRide;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides;

namespace Taxi.Api.Controllers.Rides
{
    [Route("api/rides")]
    [ApiController]
    public class RidesController : ControllerBase
    {
        private readonly ISender _sender;

        public RidesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("createRide")]
        [Authorize(Roles = "driver")]
        public async Task<IActionResult> CreateRide([FromBody] CreateRideDto dto, CancellationToken cancellationToken)
        {
            var command = new CreateRideCommand(
                dto.userId,
                dto.driverId,
                dto.startAddress,
                dto.endAddress);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("reserveRide")]
        [Authorize(Roles = "driver")]
        public async Task<IActionResult> ReserveRide([FromBody] ReserveRideDto dto, CancellationToken cancellationToken)
        {
            var command = new ReserveDriverCommand(
                dto.RideId);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        //[HttpGet("getAllRides")]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> AddRides([FromBody] AddRidesDto dto, CancellationToken cancellationToken)
        //{
        //    return Ok();
        //}

        //[HttpPost("addRides")]
        //[Authorize(Roles = "driver")]
        //public async Task<IActionResult> AddRides([FromBody] AddRidesDto dto, CancellationToken cancellationToken)
        //{
        //    return Ok();
        //}

        //[HttpPost("getAllRidesUser")]
        //[Authorize(Roles = "user")]
        //public async Task<IActionResult> AddRides([FromBody] AddRidesDto dto, CancellationToken cancellationToken)
        //{
        //    return Ok();
        //}p
    }
}
