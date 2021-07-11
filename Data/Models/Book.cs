using System;
using Boilerplate_.Net_Core_REST.Data.Models;

namespace Boilerplate.Data.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public DateTime Year { get; set; }
    }
}