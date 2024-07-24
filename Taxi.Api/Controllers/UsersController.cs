using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Taxi.Application.Dto;
using Taxi.Application.Users.Commands;

namespace Taxi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(loginRequestDto.username, loginRequestDto.password);

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] RegisterRequestDto) { }


    }
}
