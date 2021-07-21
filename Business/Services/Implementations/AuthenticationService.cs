

using AutoMapper;
using Boilerplate.Business.Constants;
using Boilerplate.Business.DTOs.Authentication;
using Boilerplate.Business.Utilities;
using Boilerplate.Data.Models;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Boilerplate_REST.Business.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapperService;

        public AuthenticationService(IJwtService jwtService, IUserService userService, IConfiguration configuration, IMapper mapperService)
        {
            _userService = userService;
            _configuration = configuration;
            _jwtService = jwtService;
            _mapperService = mapperService;
        }

        public User Authenticate(AuthenticateRequestDto requestDto)
        {
            if (string.IsNullOrEmpty(requestDto.Email) || string.IsNullOrWhiteSpace(requestDto.Password))
            {
                return null;
            }
            string hashedPassword = this.HashPassword(requestDto.Email, requestDto.Password);

            return _userService.Get(x => x.Email == requestDto.Email && x.Password == hashedPassword).SingleOrDefault();
        }


        public User RefreshToken(RefreshTokenRequestDto requestDto)
        {
            if (string.IsNullOrEmpty(requestDto.RefreshToken))
            {
                return null;
            }
            return _userService.Get(u => u.RefreshToken.Token == requestDto.RefreshToken, false, "RefreshToken").SingleOrDefault();
        }

        public string GetToken(User user)
        {
            user.RefreshToken = GenerateRefreshToken();
            user = _userService.Update(user);
            _userService.SaveChanges();
            return _jwtService.GenerateJwtToken(user);
        }

        public string GetRefreshToken(User user)
        {

            user.RefreshToken = GenerateRefreshToken();
            user = _userService.Update(user);
            _userService.SaveChanges();
            return _jwtService.GenerateJwtToken(user);
        }

        private static RefreshToken GenerateRefreshToken()
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

        public User AddUser(UserRequestDto requestDto)
        {
            var newUser = _mapperService.Map<User>(requestDto);
            newUser.Password = this.HashPassword(requestDto.Email, requestDto.Password);

            _userService.Add(newUser);
            _userService.SaveChanges();
            return newUser;
        }

        public string HashPassword(string email, string password)
        {
            return SecurityUtility.GetHashString(password + _configuration["Jwt:Key"] + email);
        }
    }
}
