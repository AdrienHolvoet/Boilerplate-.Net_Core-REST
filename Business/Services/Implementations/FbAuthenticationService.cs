


using Boilerplate.Business.ExternalDtos.Facebook;
using Boilerplate.Business.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Boilerplate_REST.Business.Services.Implementations
{
    public class FbAuthenticationService : IFbAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FbAuthenticationService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FbTokenValidationResultDto> ValidateAccessTokenAsync(string accessToken)
        {
            var formattedUrl = string.Format(_configuration["Facebook:TokenValidationUrl"], accessToken, _configuration["Facebook:AppId"], _configuration["Facebook:AppSecret"]);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FbTokenValidationResultDto>(responseAsString);
        }

        public async Task<FbUserInfoResultDto> GetUserInfoResultAsync(string accessToken)
        {
            var formattedUrl = string.Format(_configuration["Facebook:UserInfoUrl"], accessToken);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FbUserInfoResultDto>(responseAsString);
        }
    }
}
