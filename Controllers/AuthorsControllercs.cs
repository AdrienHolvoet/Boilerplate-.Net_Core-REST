using System;
using System.Collections.Generic;
using System.Linq;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate_REST.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private IBaseService<Author> _authorService;

        public AuthorsController(IBaseService<Author> authorService)
        { _authorService = authorService; }

        [HttpGet("")]
        public IEnumerable<Author> GetAllAuthors() => _authorService.GetAll();

        [HttpGet("{authorName}")]
        public Author GetAuthorByName(String authorName)
        {
            return _authorService.Get(author => author.Name == authorName).SingleOrDefault();
        }

        [HttpPost("")]
        [AllowAnonymous]
        public void AddAuthor([FromBody] Author author)
        {
            _authorService.Add(author);
            _authorService.SaveChanges();
        }
    }
}