

/*namespace Boilerplate_REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private IBaseService<Book> _bookService;
        public BooksController(IBaseService<Book> bookService)
        { this._bookService = bookService; }

        [HttpGet]
        [Route("")]
        public IEnumerable<Book> GetAllBooks() { return _bookService.GetAll(); }

        [HttpGet]
        [Route("{bookId}")]
        public Book GetBookById(Guid bookId) => _bookService.GetSingleById(bookId);

        [HttpPut]
        [Route("{bookId}")]
        public IActionResult UpdateBook(Guid bookId, [FromBody] Book book)
        {
            try
            {
                var oldItem = _bookService.GetSingleById(bookId);

                if (oldItem == null)
                    return NotFound("Book not found.");

                var updatedBook = _bookService.Update(book);
                _bookService.SaveChanges();

                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public void AddBook([FromBody] Book book)
        {
            _bookService.Add(book);
            _bookService.SaveChanges();
        }

        [HttpDelete]
        [Route("{bookId}")]
        [AllowAnonymous]
        public ActionResult DeleteBook(Guid bookId)
        {

            try
            {
                var oldItem = _bookService.GetSingleById(bookId);

                if (oldItem == null)
                    return NotFound("Book not found.");

                _bookService.DeleteById(bookId);
                _bookService.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          ;
        }
    }
}*/
