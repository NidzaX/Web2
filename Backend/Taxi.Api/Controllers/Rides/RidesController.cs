using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Taxi.Application.Dto.Commands;
using Taxi.Application.Dto.Queries;
using Taxi.Application.Rides.Commands.CreateRide;
using Taxi.Application.Rides.Commands.ReserveRide;
using Taxi.Application.Rides.Commands.ReserveRideByDriver;
using Taxi.Application.Rides.Queries.GetAllRides;
using Taxi.Application.Rides.Queries.GetAvailableRides;
using Taxi.Application.Rides.Queries.GetCompletedRides;
using Taxi.Application.Rides.Queries.GetUserRides;
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
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CreateRide([FromBody] CreateRideDto dto, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Received DTO: {JsonConvert.SerializeObject(dto)}");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Model State Error: {error.ErrorMessage}");
                }
                return BadRequest("Invalid model state.");
            }

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
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ReserveRide([FromBody] ReserveRideDto dto, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Received RideId: {dto.RideId}");

            var command = new ReserveDriverCommand(
                dto.RideId);

            Result<Ride> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("reserveRideByDriver")]
        [Authorize(Roles = "driver")]
        public async Task<IActionResult> ReserveRideByDriver([FromBody] ReserveRideByDriverDto dto, CancellationToken cancellationToken)
        {
            var driverIdFromJWT = Guid.Parse(HttpContext.User.FindFirst("Id")?.Value);

            var command = new ReserveRideByDriverCommand(dto.RideId, driverIdFromJWT);

            Result<Ride> result = await _sender.Send(command, cancellationToken);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("getAllRides")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllRides(CancellationToken cancellationToken)
        {
            var query = new GetAllRidesQuery();

            Result<List<GetAllRidesDto>> result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("getCompletedRides")]
        [Authorize(Roles = "driver")]
        public async Task<IActionResult> GetCompletedRides(CancellationToken cancellationToken)
        {
            var driverIdFromJWT = HttpContext.User.FindFirst("Id")?.Value;

            if(!Guid.TryParse(driverIdFromJWT, out Guid driverId))
            {
                return BadRequest("Invalid driver Id");
            }

            var query = new GetCompletedRidesQuery(driverId);

            Result<List<GetCompletedRidesDto>> result = await _sender.Send(query, cancellationToken);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("getAvailableRides")]
        [Authorize(Roles = "driver")]
        public async Task<IActionResult> GetAvailableRides(CancellationToken cancellationToken)
        {
            var driverIdFromJWT = HttpContext.User.FindFirst("Id")?.Value;

            var query = new GetAvailableRidesQuery();

            Result<List<GetAvailableRidesDto>> result = await _sender.Send(query, cancellationToken);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("getPreviousRides")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetPreviousRides(CancellationToken cancellationToken)
        {
            var userFromJWT = HttpContext.User.FindFirst("Id")?.Value;

            if(!Guid.TryParse(userFromJWT, out Guid userId))
            {
                return BadRequest("Invalid user Id");
            }

            var query = new GetUserRidesQuery(userId);

            Result<List<GetUserRideDto>> result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
