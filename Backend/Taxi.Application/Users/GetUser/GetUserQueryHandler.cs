using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Dto;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Users;

namespace Taxi.Application.Users.GetUser
{
    internal sealed class GetUserQueryHandler
        : IQueryHandler<GetUserQuery, GetUserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByIdAsync(request.userId);
            if (user == null)
            {
                return Result.Failure<GetUserDto>(UserErrors.NotFound); 
            }

            if (user.UserType.Value == "user")
            {
                var userDto = _mapper.Map<GetUserDto>(user);
                return Result.Success(userDto);
            }

            return Result.Failure<GetUserDto>(UserErrors.UnauthorizedAccess);
        }
    }
}
