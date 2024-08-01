using MediatR;
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

        //[HttpGet]
        //public async Task<IActionResult> S

    }
}
