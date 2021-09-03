using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Data.Models
{
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public Image Image { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Information> Informations { get; set; }
        public User User { get; set; }
    }
}
