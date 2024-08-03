using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpPost("addRide")]
        //[Authorize(Roles = "driver")]
        //public async Task<IActionResult> AddRides([FromBody] AddRidesDto dto, CancellationToken cancellationToken)
        //{
        //    return Ok();
        //}

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
        //}

    }
}
