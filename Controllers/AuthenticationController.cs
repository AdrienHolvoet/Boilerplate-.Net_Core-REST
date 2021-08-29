using AutoMapper;
using Boilerplate.Business.DTOs.Authentication;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Boilerplate_REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IMapper mapperService, ILogger<AuthenticationController> logger, IAuthenticationService authenticationService) : base(mapperService, logger)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDto requestDto)
        {
            try
            {
                var existingUser = _authenticationService.Authenticate(requestDto);

                if (existingUser == null)
                    return NotFound("User not found.");

                //Generate the Token and RefreshToken
                var token = _authenticationService.GetToken(existingUser);
                var response = _mapperService.Map<AuthenticateResponseDto>(existingUser);
                response.Token = token;

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequestDto requestDto)
        {
            try
            {
                var existingUser = _authenticationService.RefreshToken(requestDto);

                if (existingUser == null)
                    return NotFound("User not found.");

                if (existingUser.RefreshToken == null || existingUser.RefreshToken.IsExpired)
                    return Unauthorized("Invalid token.");

                //Regenerate the Token and RefreshToken
                var token = _authenticationService.GetToken(existingUser);
                var response = _mapperService.Map<AuthenticateResponseDto>(existingUser);
                response.Token = token;

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("registration")]
        public IActionResult Registration([FromBody] UserRequestDto requestDto)
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
