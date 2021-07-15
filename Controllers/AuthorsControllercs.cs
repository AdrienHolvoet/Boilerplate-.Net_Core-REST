using System;
using System.Linq;
using AutoMapper;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Boilerplate_REST.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : BaseCrudController<AuthorDto, AuthorDto, Author>
    {
        private IAuthorService _authorService;

        public AuthorsController(IMapper mapperService, ILogger<AuthorsController> logger, IAuthorService service) : base(mapperService, logger, service)
        {
            _authorService = service;
            _logger = logger;
        }

        [HttpGet("name/{authorName}")]
        public IActionResult GetAuthorByName(String authorName)
        {
            try
            {
                var author = _authorService.Get(author => author.Name == authorName).FirstOrDefault();

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