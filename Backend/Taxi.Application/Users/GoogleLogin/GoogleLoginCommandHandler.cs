using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taxi.Application.Abstractions.Messaging;
using Taxi.Application.Users.Shared;
using Taxi.Domain.Abstractions;
using Taxi.Domain.Users;

namespace Taxi.Application.Users.GoogleLogin
{
    internal sealed class GoogleLoginCommandHandler : ICommandHandler<GoogleLoginCommand, AccessTokenResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IConfigurationSection _googleClientId;
        private readonly IConfigurationSection _secretKey;

        public GoogleLoginCommandHandler(
            IUserRepository userRepository,
            IConfiguration configuration
            )
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _googleClientId = configuration.GetSection("SecretKey");
            _secretKey = configuration.GetSection("GoogleClientId"); 
        }

        public async Task<Result<AccessTokenResponse>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GoogleJsonWebSignature.ValidationSettings validationSettings = new GoogleJsonWebSignature.ValidationSettings();
                validationSettings.Audience = new List<string>() { _googleClientId.Value };

                GoogleJsonWebSignature.Payload payload = Task.Run(() => GoogleJsonWebSignature.ValidateAsync(request.Token, validationSettings)).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            User? user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("User with this email doesn't exist");
            }

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Role, "user"));
            claims.Add(new Claim("username", user.Username.Value));
            claims.Add(new Claim("verified", user.Verified.ToString()));
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:44306", //url servera koji je izdao token
                claims: claims, //claimovi
                expires: DateTime.Now.AddMinutes(20), //vazenje tokena u minutama
                signingCredentials: signinCredentials //kredencijali za potpis
            );
           


            return new AccessTokenResponse(new JwtSecurityTokenHandler().WriteToken(tokeOptions).ToString());

        }
    }
}