using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
 //       [Authorize(Roles = "driver")]
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
