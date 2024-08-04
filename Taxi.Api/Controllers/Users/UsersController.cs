using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Taxi.Application.Dto;
using Taxi.Application.Users.ChangeUserPassword;
using Taxi.Application.Users.Commands;
using Taxi.Application.Users.EditUser;
using Taxi.Application.Users.GetUser;
using Taxi.Application.Users.GoogleLogin;
using Taxi.Application.Users.LogInUser;
using Taxi.Application.Users.VerifyDriver;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Users;

namespace Taxi.Api.Controllers.Users
{
    [Route("api/users")]
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
            var command = new LoginUserCommand(loginRequestDto.Username, loginRequestDto.Password);

            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDto registerRequestDto, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                registerRequestDto.Username,
                registerRequestDto.FirstName,
                registerRequestDto.LastName,
                registerRequestDto.Password,
                registerRequestDto.Address,
                registerRequestDto.Birthday,
                registerRequestDto.UserType,
                registerRequestDto.Email,
                registerRequestDto.File);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);

        }

        [HttpPost("registerGoogleUser")]
        public async Task<IActionResult> RegisterGoogleUser([FromForm] RegisterUserGoogleDto dto, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                dto.Username,
                dto.FirstName,
                dto.LastName,
                dto.Password,
                dto.Address,
                dto.Birthday,
                dto.UserType,
                dto.Email,
                dto.File);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("loginGoogle")]
        public async Task<IActionResult> LoginGoogleUser([FromBody] GoogleLoginDto dto, CancellationToken cancellationToken)
        {
            var command = new GoogleLoginCommand(dto.Email, dto.Token);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.IsSuccess);

        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDto changePasswordRequestDto, CancellationToken cancellationToken)
        {

            var command = new ChangeUserPasswordCommand(changePasswordRequestDto.Email, changePasswordRequestDto.NewPassword, changePasswordRequestDto.OldPassword);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.IsSuccess);
        }

        [HttpPut("updateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserEditDto dto, CancellationToken cancellationToken)
        {
            var command = new EditUserCommand(
                dto.Username,
                dto.FirstName,
                dto.LastName,
                dto.Password,
                dto.Address,
                dto.Birthday,
                dto.UserType,
                dto.Email,
                dto.File);

            var result = await _sender.Send(command, cancellationToken);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.IsSuccess);
        }

        [HttpPut("verify/{email}/{v}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> VerifyUser([FromForm] VerifyDriverDto verifyDriverDto, CancellationToken cancellationToken)
        {
            var command = new VerifyDriverCommand(verifyDriverDto.email, verifyDriverDto.v);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.IsSuccess);
        }

        [HttpGet("getUserData/{email}")]
        public async Task<IActionResult> GetUserData(string email, CancellationToken cancellationToken)
        {
            var query = new GetUserQuery(email);

            Result<GetUserDto> result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);

            }
            return Ok(result.IsSuccess);
        }

        //[HttpGet("getDriverData/{email}")]
        //public async Task<IActionResult> GetDriverData(string email, CancellationToken cancellationToken)
        //{
        //    var query = new GetUserQuery(email);

        //    Result<GetUserDto> result = await _sender.Send(query, cancellationToken);

        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);

        //    }
        //    return Ok(result.IsSuccess);

        //}
    }
}
