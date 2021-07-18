using System;
using System.Linq;
using AutoMapper;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Boilerplate_REST.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : BaseCrudController<AuthorDto, AuthorDto, Author>
    {
        private IAuthorService _authorService;

        public AuthorsController(IMapper mapperService, ILogger<AuthorsController> logger, IAuthorService service) : base(mapperService, logger, service, "Books")
        {
            _authorService = service;
            _logger = logger;
        }

        [HttpGet("name/{authorName}")]
        public IActionResult GetAuthorByName(string authorName)
        {
            var currentUser = HttpContext.User;
            try
            {
                var author = _authorService.Get(author => author.Name == authorName).FirstOrDefault();

                if (author == null)
                {
                    return NotFound("Author not found.");
                }

                return Ok(_mapperService.Map<AuthorDto>(author));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
