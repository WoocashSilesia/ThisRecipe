using ThisRecipe.Domain.Common;
using System.Collections.Generic;
using static ThisRecipe.Domain.Helpers.Helpers;
using System;

namespace ThisRecipe.Domain.Entity
{
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public RecipeSource RecipeSourceType { get; set; }
        public List<Ingredient> Ingrediens { get; set; }
        public List<Step> Steps { get; set; }
        public List<string> KitchenStuff { get; set; }
        public short PreparationDifficulty { get; set; }
        public short NumberOfServings { get; set; }
        public short PreparationTime { get; set; }

        public Recipe(
            int id,
            string title,
            string author,
            string descripton,
            RecipeSource recipeSource,
            List<Ingredient> ingrediens,
            List<string> thingsToPrepare,
            List<Step> steps,
            short preparationDifficulty,
            short numberOfServings,
            short preparationTime)
        {
            Id = id;
            Title = title;
            Author = author;
            Description = descripton;
            RecipeSourceType = recipeSource;
            Ingrediens = ingrediens;
            KitchenStuff = thingsToPrepare;
            Steps = steps;
            NumberOfServings = numberOfServings;
            PreparationTime = preparationTime;
            PreparationDifficulty = preparationDifficulty;
        }
    }
}
