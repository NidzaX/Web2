using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Users.Commands;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Users;

namespace Taxi.Application.Users.RegisterUserGoogle
{
    internal sealed class RegisterUserGoogleCommandHandler : ICommandHandler<RegisterUserGoogleCommand, Guid>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfigurationSection _googleClientId;


        public RegisterUserGoogleCommandHandler(
        IAuthenticationService authenticationService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IConfiguration config)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _googleClientId = config.GetSection("GoogleClientId");
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
            RegisterUserGoogleCommand request,
            CancellationToken cancellationToken)
        {
            string token = request.Token;

            try
            {
                GoogleJsonWebSignature.ValidationSettings validationSettings = new GoogleJsonWebSignature.ValidationSettings();
                validationSettings.Audience = new List<string>() { _googleClientId.Value };

                GoogleJsonWebSignature.Payload payload = Task.Run(() => GoogleJsonWebSignature.ValidateAsync(token, validationSettings)).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (request.Address.Trim() == "" || request.Email.Trim() == "" || request.LastName.Trim() == "" || request.Username.Trim() == "")
                return (Result<Guid>)Result.Failure(UserErrors.NotFound);

            if (request.UserType.Trim() != "user")
                return (Result<Guid>)Result.Failure(UserErrors.InvalidCredentials);

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            using (var stream = new MemoryStream())
            {
                var user = User.Create(
                       new FirstName(request.FirstName),
                       new LastName(request.LastName),
                       new Email(request.Email),
                       new Username(request.Username),
                       new Password(hashPassword),
                       new Address(request.Address),
                       new Birthday(request.Birthday),
                       new UserType(request.UserType),
                       new Picture(stream.ToArray())
                       );
                       

                _userRepository.Add(user);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return user.Id;
            
            }
        }
    }
}
