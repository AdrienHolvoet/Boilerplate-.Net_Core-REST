using AutoMapper;
using Boilerplate.Business.Dtos;
using Boilerplate.Business.Services.Interfaces;
using Boilerplate.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseCrudController<UserRequestDto, UserResponseDto, User>
    {
        public UsersController(IMapper mapperService, ILogger<UsersController> logger, IBaseService<User> userService) : base(mapperService, logger, userService, "")
        {

        }
    }
}
