using Boilerplate.Business.Dtos;
using Boilerplate.Business.Dtos.Authentication;
using Boilerplate.Data.Models;
using Google.Apis.Auth;
using System.Threading.Tasks;

namespace Boilerplate.Business.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public User Authenticate(AuthenticateRequestDto requestDto);
        public User RefreshToken(RefreshTokenRequestDto token);
        public string GetToken(User user);
        public User AddUser(UserRequestDto requestDto);
        public Task<User> AuthenticateWithFacebookAsync(string token);
        public User AuthenticateWithGoogleAsync(GoogleJsonWebSignature.Payload payload);
    }
}
