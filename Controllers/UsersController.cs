using AutoMapper;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Boilerplate_REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseCrudController<UserRequestDto, UserResponseDto, User>
    {
        private readonly IAuthenticationService _authenticationService;
        public UsersController(IMapper mapperService, ILogger<UsersController> logger, IBaseService<User> userService, IAuthenticationService authenticationService) : base(mapperService, logger, userService, "")
        {
            _authenticationService = authenticationService;
        }
    }
}
