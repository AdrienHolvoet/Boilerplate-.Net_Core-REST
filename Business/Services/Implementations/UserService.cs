using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Interfaces;
using Boilerplate_REST.Data.Models;

namespace Boilerplate_REST.Business.Services.Implementations
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IRepository<User> repository) : base(repository)
        {
        }
    }
}
