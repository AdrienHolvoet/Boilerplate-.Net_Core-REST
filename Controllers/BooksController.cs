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
    public class BooksController : BaseCrudController<BookDto, BookDto, Book>
    {
        public BooksController(IMapper mapperService, ILogger<BooksController> logger, IBookService bookService) : base(mapperService, logger, bookService,"")
        {

        }
    }
}
