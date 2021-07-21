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

        [HttpPost]
        public override IActionResult Post([FromBody] UserRequestDto requestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var newUser = _authenticationService.AddUser(requestDto);

                return Ok(_mapperService.Map<UserResponseDto>(newUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
