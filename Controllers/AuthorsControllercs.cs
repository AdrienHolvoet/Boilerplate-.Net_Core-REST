using System;
using System.Linq;
using AutoMapper;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate_REST.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : BaseCrudController<AuthorDto, AuthorDto, Author>
    {
        private IAuthorService _authorService;

        public AuthorsController(IMapper mapperService, IAuthorService service) : base(mapperService, service)
        {
            _authorService = service;
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

                return BadRequest(ex.Message);
            }
        }
    }
}