using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Rides;
using Taxi.Domain.Users;

namespace Taxi.Application.Users.Commands
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterUserCommandHandler> _logger;


        public RegisterUserCommandHandler(
       IAuthenticationService authenticationService,
       IUserRepository userRepository,
       IUnitOfWork unitOfWork,
       ILogger<RegisterUserCommandHandler> logger) 

        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /*
        string username,
        string firstName,
        string lastName,
        string password,
        string address,
        DateTime Birthday,
        string UserType,
        string Email,
        IFormFile file
        */

        public async Task<Result<Guid>> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Log the incoming request
                _logger.LogInformation("Received user registration request: Username = {Username}, Email = {Email}",
                    request.Username, request.Email);

                // Validate inputs
                if (string.IsNullOrWhiteSpace(request.Address) || string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.LastName) || string.IsNullOrWhiteSpace(request.Username))
                {
                    throw new Exception("Required fields are missing.");
                }

                if (request.Address.Trim() == "" || request.Email.Trim() == "" || request.LastName.Trim() == "" || request.Username.Trim() == "")
                    return (Result<Guid>)Result.Failure(UserErrors.NotFound);

                if (request.UserType.Trim() != "user" && request.UserType.Trim() != "driver")
                    return (Result<Guid>)Result.Failure(UserErrors.InvalidCredentials);


                var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                using (var stream = new MemoryStream())
                {
                    request.File.CopyTo(stream);

                    var user = User.Create(
                        new FirstName(request.FirstName),
                        new LastName(request.LastName),
                        new Email(request.Email),
                        new Username(request.Username),
                        new Password(hashPassword),
                        new Address(request.Address),
                        new Birthday(request.Birthday.ToUniversalTime()),
                        new UserType(request.UserType),
                        new Picture(stream.ToArray())
                    );

                    _userRepository.Add(user);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("User registered successfully with ID: {UserId}", user.Id);

                    return user.Id;
                }

            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred while registering the user");

                throw new Exception("An error occurred during registration.");
            }
        }
    }
}
