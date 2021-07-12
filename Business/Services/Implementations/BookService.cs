using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Interfaces;
using Boilerplate_REST.Data.Models;

namespace Boilerplate_REST.Business.Services.Implementations
{
    public class BookService : BaseService<Book>, IBookService
    {
        public BookService(IRepository<Book> repository) : base(repository)
        {
        }
    }
}