using System;
using System.Collections.Generic;
using System.Linq;
using ThisRecipe.App.Concrete;
using ThisRecipe.App.Extensions;
using ThisRecipe.Domain.Entity;
using static ThisRecipe.Domain.Helpers.Helpers;

namespace ThisRecipe.App.Managers
{
    public class SingleRecipeManager
    {
        private readonly IngredientManager _ingredientManager;
        private readonly SingleRecipeService _singleRecipeService;
        public SingleRecipeManager(IngredientManager im)
        {
            _ingredientManager = im;
            _singleRecipeService = new SingleRecipeService();
        }

        public SingleRecipe CreateSingleRecipe()
        {
            Console.WriteLine("\n-----RECIPE NAME-----");
            var name = SetName();
            Console.WriteLine("\n-----RECIPE SOURCE-----");
            var recipeSource = SetSourceOfRecipe();
            Console.WriteLine("\n-----RECIPE INGREDIENTS-----");
            var ingredients = SetIngredients();
            var singleRecipe = new SingleRecipe(
                _singleRecipeService.GetLastId(),
                name, recipeSource, ingredients
                );
            _singleRecipeService.AddItem(singleRecipe);
            return singleRecipe;
        }

        public SingleRecipe AddExistedSingleRecipe(List<SingleRecipe>  currentRecipes)
        {
            var recipes = _singleRecipeService.GetAllItems();
            bool isOk = false;

            while (!isOk)
            {
                Console.WriteLine("Select recipe to add");
                foreach (var (recipe, index) in recipes.Where(r => !currentRecipes.Any(xr => xr.Id == r.Id)).WithIndex())
                {
                    Console.WriteLine($"{index + 1}) {recipe.Name}");
                }
                isOk = int.TryParse(Console.ReadLine(), out int recipeNumber);
                if (recipes.ElementAtOrDefault(recipeNumber - 1) != null)
                {
                    return recipes.ElementAt(recipeNumber - 1);
                }
                else
                {
                    isOk = false;
                }
            }

            return null;
        }

        private List<Ingredient> SetIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            bool stop = false;
            while (!stop)
            {
                Console.WriteLine("Add ingredient");
                ingredients.Add(_ingredientManager.AddIngredient());

                while (true)
                {
                    Console.WriteLine("1) Add another ingredient");
                    Console.WriteLine("2) Exit");
                    bool isParsed = int.TryParse(Console.ReadLine(), out int continueOption);
                    if (continueOption == 2 || continueOption == 1)
                    {
                        if (continueOption == 2)
                        {
                            stop = true;
                        }
                        break;
                    }
                }
            }

            return ingredients;
        }

        private string SetName()
        {
            Console.Write("Please insert name: ");
            return Console.ReadLine();
        }

        private RecipeSource SetSourceOfRecipe()
        {
            bool isCorrect;
            Console.Write("Please select number of one source of recipe from list: \n");
            ShowRecipeSource();
            isCorrect = Enum.TryParse(Console.ReadLine(), out RecipeSource RecipeSource);
            while (!isCorrect)
            {
                Console.WriteLine("Can't find source. Please enter again source");
                ShowRecipeSource();
                isCorrect = Enum.TryParse(Console.ReadLine(), out RecipeSource);
            }
            return RecipeSource;
        }

        private void ShowRecipeSource()
        {
            int i = 0;
            foreach (RecipeSource recipeSource in Enum.GetValues(typeof(RecipeSource)))
            {
                string recipeSourceName = recipeSource == RecipeSource.SocialMedia ? "Social media" : recipeSource.ToString().FirstCharToUpper();
                Console.WriteLine($" {++i}) {recipeSourceName}");
            }
        }
    }
}
