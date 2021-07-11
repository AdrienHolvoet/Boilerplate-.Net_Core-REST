using System;
using System.Collections.Generic;
using Boilerplate_.Net_Core_REST.Data.Models;

namespace Boilerplate.Data.Models
{
    public class Author : BaseEntity
    {
        public String Name { get; set; }
        public String Country { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}