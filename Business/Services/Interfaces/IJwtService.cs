using Boilerplate.Data.Models;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Data.Models;

namespace Boilerplate_REST.Business.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateJwtToken(User user);
    }
}
