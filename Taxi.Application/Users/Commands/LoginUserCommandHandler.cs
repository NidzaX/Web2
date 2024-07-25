using MediatR;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Taxi.Application.Users.Commands;

internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessTokenResponse>
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        
    }

    public async Task<Result<AccessTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception("Incorrect login credentials");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password.Value))
        {
            return new AccessTokenResponse("Error");
        }

        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim("Id", user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Role, user.UserTypes.ToString()));

       // if (user.UserTypes == UserType.Driver)
       // {
      //      claims.Add(new Claim("VerificationStatus", user.VerificationStatus.ToString()));
       // }

        string? secretKeyValue = _configuration.GetSection("tempsecret").Value;
        if (secretKeyValue == null)
        {
            throw new Exception("Secret key is not set properly");
        }

        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyValue));
        SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: "http://localhost:44319",
            claims: claims,
            expires: DateTime.Now.AddYears(1),
            signingCredentials: signingCredentials
        );

        return new AccessTokenResponse(new JwtSecurityTokenHandler().WriteToken(securityToken).ToString());
    }
}
