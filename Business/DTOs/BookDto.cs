using System;

namespace Boilerplate.Business.DTOs
{
    public class BookDto : BaseDto
    {
        public string Title { get; set; }
        public DateTime Year { get; set; }
    }
}