﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taxi.Application.Dto.Commands;
using Taxi.Application.Dto.Queries;
using Taxi.Application.Users.Commands.ChangeUserPassword;
using Taxi.Application.Users.Commands.EditUser;
using Taxi.Application.Users.Commands.GoogleLogin;
using Taxi.Application.Users.Commands.LogInUser;
using Taxi.Application.Users.Commands.RegisterUser;
using Taxi.Application.Users.Commands.VerifyDriver;
using Taxi.Application.Users.Queries.GetUser;
using Taxi.Domain.Abstractions;

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
            try
            {
                if (registerRequestDto.File == null || registerRequestDto.File.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }

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
        public async Task<IActionResult> UpdateProfile([FromForm] UserEditDto dto, CancellationToken cancellationToken)
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

        [HttpPut("verify")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> VerifyUser([FromBody] VerifyDriverDto verifyDriverDto, CancellationToken cancellationToken)
        {
            var command = new VerifyDriverCommand(verifyDriverDto.email, verifyDriverDto.v);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.IsSuccess);
        }

        [HttpGet("getUserData/{userId}")]
        public async Task<IActionResult> GetUserData(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserQuery(userId);

            Result<GetUserDto> result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);

            }
            return Ok(result.Value);
        }

        [HttpGet("getDriverData")]
        public async Task<IActionResult> GetDriverData(CancellationToken cancellationToken)
        {
            var userFromJWT = HttpContext.User.FindFirst("Id")?.Value;

            if (!Guid.TryParse(userFromJWT, out Guid userId))
            {
                return BadRequest();
            }

            var query = new GetUserQuery(userId);

            Result<GetUserDto> result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);

            }
            return Ok(result.Value);

        }
    }
}
