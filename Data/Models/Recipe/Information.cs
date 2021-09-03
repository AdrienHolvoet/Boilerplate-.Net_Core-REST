

using System;

namespace Boilerplate.Data.Models
{
    public class Information : BaseEntity
    {
        public string Data { get; set; }
        public string Unit { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

    }
}
