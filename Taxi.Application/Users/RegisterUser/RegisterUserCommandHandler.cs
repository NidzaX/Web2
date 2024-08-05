using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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


       public RegisterUserCommandHandler(
       IAuthenticationService authenticationService,
       IUserRepository userRepository,
       IUnitOfWork unitOfWork)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
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

            //Check if user exists
            if (request.address.Trim() == "" || request.email.Trim() == "" || request.lastName.Trim() == "" || request.username.Trim() == "")
                return (Result<Guid>)Result.Failure(UserErrors.NotFound);

            if (request.userType.Trim() != "user" && request.userType.Trim() != "driver")
                return (Result<Guid>)Result.Failure(UserErrors.InvalidCredentials);


            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.password);

            using (var stream = new MemoryStream())
            {
                request.file.CopyTo(stream);

                var user = User.Create(
                    new FirstName(request.firstName),
                    new LastName(request.lastName),
                    new Email(request.email),
                    new Username(request.username),
                    new Password(hashPassword),
                    new Address(request.address),
                    new Birthday(request.birthday),
                    new UserType(request.userType),
                    new Picture(stream.ToArray())
                );

                _userRepository.Add(user);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return user.Id;
            }
        }
    }
}
