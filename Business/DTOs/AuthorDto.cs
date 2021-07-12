using System;
using System.Collections.Generic;
using Boilerplate_REST.Data.Models;

namespace Boilerplate_REST.Business.DTOs
{
    public class AuthorDto : BaseDto
    {
        public string Name { get; set; }
        public String Country { get; set; }
        public virtual List<Book> Books { get; set; }
    }
}