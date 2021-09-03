using System;
using System.Collections.Generic;
using Boilerplate.Data.Models;

namespace Boilerplate.Business.Dtos
{
    public class RecipeDto : BaseDto
    {
        public string Title { get; set; }
        public ImageDto Image { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public ICollection<IngredientDto> Ingredients { get; set; }
        public ICollection<InformationDto> Informations { get; set; }
    }
}
