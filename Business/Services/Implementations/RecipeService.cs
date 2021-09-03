using Boilerplate.Business.Services.Interfaces;
using Boilerplate.Data.Interfaces;
using Boilerplate.Data.Models;

namespace Boilerplate.Business.Services.Implementations
{
    public class RecipeService : BaseService<Recipe>, IRecipeService
    {
        public RecipeService(IRepository<Recipe> repository) : base(repository)
        {
        }
        //Add new business logic
    }
}