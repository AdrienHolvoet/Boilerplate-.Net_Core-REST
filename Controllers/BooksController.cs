using AutoMapper;
using Boilerplate_REST.Business.DTOs;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate_REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : BaseCrudController<BookDto, BookDto, Book>
    {
        IBookService _bookService;
        public BooksController(IMapper mapperService, IBookService bookService) : base(mapperService, bookService)
        {
            _bookService = bookService;
        }
    }
}