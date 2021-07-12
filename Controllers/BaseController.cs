using AutoMapper;

namespace Boilerplate.Controllers
{
    public class BaseController
    {
        protected IMapper _mapperService;

        public BaseController(IMapper mapperService
                              )
        {
            _mapperService = mapperService;
        }

    }
}