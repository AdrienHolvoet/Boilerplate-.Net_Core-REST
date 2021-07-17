using AutoMapper;
using Boilerplate.Business.DTOs.Authentication;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Boilerplate_REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private IAuthenticationService _authenticationService;
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

                if (existingUser.RefreshToken.IsExpired)
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


    }
}
