using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Users;

namespace Taxi.Application.Users.Commands.ChangeUserPassword
{
    internal sealed class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeUserPasswordCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByEmailAsync(request.Email);
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            if (user == null)
            {
                throw new Exception("Incorrect login credentials");
            }

            if (BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password.Value))
            {

                user.Password = new Password(hashPassword);

                _userRepository.Update(user);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            else
            {
                throw new Exception("Invalid password!");
            }
        }
    }
}
