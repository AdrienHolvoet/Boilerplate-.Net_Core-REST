using System;
using System.Collections.Generic;

namespace Boilerplate_REST.Data.Models
{
    public class Author : BaseEntity
    {
        public String Name { get; set; }
        public String Country { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}