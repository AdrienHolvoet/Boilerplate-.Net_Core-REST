using Boilerplate.Business.Services.Interfaces;
using Boilerplate.Data.Interfaces;
using Boilerplate.Data.Models;

namespace Boilerplate.Business.Services.Implementations
{
    public class ImageService : BaseService<Image>, IImageService
    {
        public ImageService(IRepository<Image> repository) : base(repository)
        {
        }
    }
}
