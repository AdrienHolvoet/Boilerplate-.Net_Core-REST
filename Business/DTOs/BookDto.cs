using System;

namespace Boilerplate_REST.Business.DTOs
{
    public class BookDto : BaseDto
    {
        public string Title { get; set; }
        public DateTime Year { get; set; }
    }
}