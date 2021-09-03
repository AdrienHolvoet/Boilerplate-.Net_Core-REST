using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace Boilerplate.Controllers
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

        /// <summary>Get the Guid of the current connected user.</summary>
        /// Or just use var currentUser = HttpContext.User;
        protected Guid GetAuthenticatedIdentity()
        {
            var guid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.Parse(guid);
        }

    }
}
