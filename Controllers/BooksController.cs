using System;
using System.Collections.Generic;
using Boilerplate.Data.Models;
using Boilerplate_.Net_Core_REST.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate_.Net_Core_REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private IRepository<Book> _bookRepository;
        public BooksController(IRepository<Book> bookRepository)
        { this._bookRepository = bookRepository; }

        [HttpGet]
        [Route("")]
        public IEnumerable<Book> GetAllBooks() { return _bookRepository.GetAll(); }

        [HttpGet]
        [Route("{bookId}")]
        public Book GetBookById(Guid bookId) => _bookRepository.GetById(bookId);

        [HttpPut]
        [Route("{bookId}")]
        public Book UpdateBook(Guid bookId, [FromBody] Book book)
        {
            var updatedBook = _bookRepository.Update(book);
            _bookRepository.Commit();
            return book;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public void AddBook([FromBody] Book book)
        {
            _bookRepository.Insert(book);
            _bookRepository.Commit();
        }

        [HttpDelete]
        [Route("{bookId}")]
        [AllowAnonymous]
        public void DeleteBook(Guid bookId)
        {
            _bookRepository.Delete(bookId);
            _bookRepository.Commit();
        }
    }
}