using System;
using System.Collections.Generic;

namespace Boilerplate_REST.Data.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}