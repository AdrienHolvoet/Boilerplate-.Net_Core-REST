using Boilerplate.Business.ExternalDtos.Facebook;
using System.Threading.Tasks;

namespace Boilerplate.Business.Services.Interfaces
{
    public interface IFbAuthenticationService
    {
        Task<FbTokenValidationResultDto> ValidateAccessTokenAsync(string accessToken);

        Task<FbUserInfoResultDto> GetUserInfoResultAsync(string accessToken);
    }
}
