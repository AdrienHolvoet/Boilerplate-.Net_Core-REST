using System;

namespace Boilerplate_REST.Data.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public DateTime Year { get; set; }
    }
}