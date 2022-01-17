using System;
using ThisRecipe.App.Concrete;
using ThisRecipe.Domain.Entity;

namespace ThisRecipe.App.Managers
{
    public class IngredientManager
    {
        private readonly IngredientService _ingredientService;
        public IngredientManager()
        {
            _ingredientService = new IngredientService();
        }

        //public Ingredient AddIngredient()
        //{
        //    Console.WriteLine("Write name of ingredient");


        //}
    }
}
