using System;
using System.Collections.Generic;
using System.Linq;
using Boilerplate.Data.Models;
using Boilerplate_.Net_Core_REST.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate_.Net_Core_REST.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private IRepository<Author> _authorRepository;

        public AuthorsController(IRepository<Author> authorRepository)
        { _authorRepository = authorRepository; }

        [HttpGet("")]
        public IEnumerable<Author> GetAllAuthors() => _authorRepository.GetAll();

        [HttpGet("{authorName}")]
        public Author GetAuthorByName(String authorName)
        {
            return _authorRepository.GetQueryable(author => author.Name == authorName).SingleOrDefault();
        }

        [HttpPost("")]
        [AllowAnonymous]
        public void AddAuthor([FromBody] Author author)
        {
            _authorRepository.Insert(author);
            _authorRepository.Commit();
        }
    }
}