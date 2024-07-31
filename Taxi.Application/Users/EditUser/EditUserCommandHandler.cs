using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //string Username,
            //string FirstName,
            //string LastName,
            //string Password,
            //string Address,
            //DateTime Birthday,
            //string UserType,
            //string Email,
            //string File,
            //bool Verified

            user.Username = new Username(request.Username);
            user.FirstName = new FirstName(request.FirstName);
            user.LastName = new LastName(request.LastName);
            user.Password = new Password(request.Password);
            user.Address = new Address(request.Address);
            user.Birthday = new Birthday(request.Birthday);
            user.UserType = new UserType(request.UserType);
            user.Email = new Email(request.Email);
            //user.Picture = new Picture(request.File);

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
