using System;

namespace Boilerplate.Data.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public User User { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
