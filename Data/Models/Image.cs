using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Data.Models
{
    public class Image : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
