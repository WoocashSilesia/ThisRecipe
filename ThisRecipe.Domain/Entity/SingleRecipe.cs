using System.Collections.Generic;
using ThisRecipe.Domain.Common;
using static ThisRecipe.Domain.Helpers.Helpers;

namespace ThisRecipe.Domain.Entity
{
    public class SingleRecipe : BaseEntity
    {
        public string Name { get; set; }
        public RecipeSource RecipeSourceType { get; set; } // Dod
        public List<Ingredient> Ingrediens { get; set; }

        public SingleRecipe(
            int id,
            string name,
            RecipeSource recipeSource,
            List<Ingredient> ingrediens)
        {
            Id = id;
            Name = name;
            RecipeSourceType = recipeSource;
            Ingrediens = ingrediens;
        }
    }
}
