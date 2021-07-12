using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate_REST.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected IMapper _mapperService;

        public BaseController(IMapper mapperService)
        {
            _mapperService = mapperService;
        }

    }
}