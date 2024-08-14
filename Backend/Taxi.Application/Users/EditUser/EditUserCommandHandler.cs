using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Users;

namespace Taxi.Application.Users.EditUser
{
    internal sealed class EditUserCommandHandler : ICommandHandler<EditUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {

            User? user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {

                throw new Exception("the user does not exist");
            }

            user.Username = new Username(request.Username);
            user.FirstName = new FirstName(request.FirstName);
            user.LastName = new LastName(request.LastName);
            user.Password = new Password(request.Password);
            user.Address = new Address(request.Address);
            user.Birthday = new Birthday(request.Birthday.ToUniversalTime());
            user.UserType = new UserType(request.UserType);
            user.Email = new Email(request.Email);
            using (var stream = new MemoryStream())
            {
                request.File.CopyTo(stream);
                user.Picture = new Picture(stream.ToArray());
            }

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
