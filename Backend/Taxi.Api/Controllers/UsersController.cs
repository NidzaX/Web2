using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Taxi.Application.Dto;
using Taxi.Application.Users.Commands;
using Taxi.Domain.Abstractions;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(loginRequestDto.username, loginRequestDto.password);

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                registerRequestDto.username,
                registerRequestDto.firstName,
                registerRequestDto.lastName,
                registerRequestDto.password,
                registerRequestDto.address,
                registerRequestDto.birthday,
                registerRequestDto.userType,
                registerRequestDto.email,
                registerRequestDto.file);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);


        }


    }
}
