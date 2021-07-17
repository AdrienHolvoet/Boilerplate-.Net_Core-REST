

using Boilerplate.Business.Constants;
using Boilerplate.Business.DTOs.Authentication;
using Boilerplate.Data.Models;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Models;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Boilerplate_REST.Business.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;


        public AuthenticationService(IJwtService jwtService, IUserService userService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public User Authenticate(AuthenticateRequestDto requestDto)
        {
            if (string.IsNullOrEmpty(requestDto.Email) || string.IsNullOrWhiteSpace(requestDto.Password))
            {
                return null;
            }
            return _userService.Get(x => x.Email == requestDto.Email && x.Password == requestDto.Password).SingleOrDefault();
        }

        public User RefreshToken(RefreshTokenRequestDto requestDto)
        {
            if (string.IsNullOrEmpty(requestDto.RefreshToken))
            {
                return null;
            }
            return _userService.Get(u => u.RefreshToken.Token == requestDto.RefreshToken).SingleOrDefault();
        }

        public string GetToken(User user)
        {

            user.RefreshToken = generateRefreshToken();
            user = _userService.Update(user);
            _userService.SaveChanges();
            return _jwtService.GenerateJwtToken(user);
        }
        public string getRefreshToken(User user)
        {

            user.RefreshToken = generateRefreshToken();
            user = _userService.Update(user);
            _userService.SaveChanges();
            return _jwtService.GenerateJwtToken(user);
        }




        private RefreshToken generateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(Authentication.REFRESH_TOKEN_DURATION),
            };
        }
    }
}
