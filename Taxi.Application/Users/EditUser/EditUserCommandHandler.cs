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

            //if (user == null)
            //{

            //    throw new Exception("The user does not exist");
            //}

            // user.FirstName.Value = request.FirstName;


           //user = User.Create(
           //            new FirstName(request.firstName),
           //            new LastName(request.lastName),
           //            new Email(request.email),
           //            new Username(request.username),
           //            new Password(hashPassword),
           //            new Address(request.address),
           //            new Birthday(request.birthday),
           //            new UserType(request.userType),
           //            null,
           //            new Verified(request.Verified));

            throw new NotImplementedException();
        }
    }
}
