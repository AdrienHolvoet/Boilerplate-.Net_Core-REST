using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Interfaces;
using Boilerplate_REST.Data.Models;

namespace Boilerplate_REST.Business.Services.Implementations
{
    public class AuthorService : BaseService<Author>, IAuthorService
    {
        public AuthorService(IRepository<Author> repository) : base(repository)
        {
        }
        //Add new business logic
    }
}