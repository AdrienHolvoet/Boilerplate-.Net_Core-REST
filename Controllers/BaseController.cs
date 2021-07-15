using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Boilerplate_REST.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected IMapper _mapperService;
        protected ILogger _logger;

        public BaseController(IMapper mapperService, ILogger logger)
        {
            _mapperService = mapperService;
            _logger = logger;
        }

    }
}