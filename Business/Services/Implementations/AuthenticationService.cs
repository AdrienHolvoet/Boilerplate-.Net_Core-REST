

using AutoMapper;
using Boilerplate.Business.Constants;
using Boilerplate.Business.Services.Interfaces;
using Boilerplate.Data.Models;
using Boilerplate.Helpers;

using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Boilerplate.Business.Dtos.Authentication;
using Boilerplate.Business.Dtos;

namespace Boilerplate.Business.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtService _jwtService;
        private readonly IBaseService<User> _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapperService;
        private readonly IFbAuthenticationService _fbAuthenticationService;

        public AuthenticationService(IFbAuthenticationService fbAuthenticationService, IJwtService jwtService, IBaseService<User> userService, IConfiguration configuration, IMapper mapperService)
        {
            _userService = userService;
            _configuration = configuration;
            _jwtService = jwtService;
            _mapperService = mapperService;
            _fbAuthenticationService = fbAuthenticationService;
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

        public async Task<User> AuthenticateWithFacebookAsync(string accesToken)
        {
            var validateTokenResult = await _fbAuthenticationService.ValidateAccessTokenAsync(accesToken);

            if (!validateTokenResult.Data.IsValid)
            {
                return null;
            }

            var userInfo = await _fbAuthenticationService.GetUserInfoResultAsync(accesToken);

            var user = _userService.Get(x => x.Email == userInfo.Email && x.Password == this.HashPassword(userInfo.Email, "")).SingleOrDefault();

            if (user == null)
            {
                //register him
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Password = ""
                };

                user.Password = this.HashPassword(user.Email, user.Password);
                _userService.Add(user);
                _userService.SaveChanges();
            }
            return user;
        }

        public User AuthenticateWithGoogleAsync(GoogleJsonWebSignature.Payload payload)
        {
            var user = _userService.Get(x => x.Email == payload.Email && x.Password == this.HashPassword(payload.Email, " ")).SingleOrDefault();

            if (user == null)
            {
                //register him
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = payload.Email,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    Password = " "
                };

                user.Password = this.HashPassword(user.Email, user.Password);
                _userService.Add(user);
                _userService.SaveChanges();
            }
            return user;
        }
    }
}
