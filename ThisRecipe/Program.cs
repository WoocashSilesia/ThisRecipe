using System;
using ThisRecipe.App.Concrete;
using ThisRecipe.App.Extensions;
using ThisRecipe.App.Managers;

namespace ThisRecipe
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuActionService menuActonService = new MenuActionService();
            IngredientManager ingredientManager = new IngredientManager();
            SingleRecipeManager singleRecipeManager = new SingleRecipeManager(ingredientManager);
            RecipeManager recipeManager = new RecipeManager(singleRecipeManager);

            var mainMenu = menuActonService.GetMenuActionsByMenuName("Main");
            bool endLoop = false;
            Console.WriteLine("Welcome to the MyRecipeApp!");

            while (!endLoop)
            {
                Console.WriteLine("Please let me know what do you want to do:");
                foreach ((var menu, int index) in mainMenu.WithIndex())
                {
                    Console.WriteLine($"{menu.Id}: {menu.Name}");
                    if(index + 1 == mainMenu.Count)
                    {
                        Console.WriteLine();
                    }
                }
                Console.Write("Option: ");
                var selectedOperation = Console.ReadKey();
           
                switch (selectedOperation.KeyChar)
                {
                    case '1':
                        var newId = recipeManager.AddNewRecipe();
                        break;
                    case '2':
                        var id = recipeManager.EditRecipe();
                        break;
                    case '3':
                        Console.WriteLine("Not done yet");
                        break;
                    case '4':
                        recipeManager.ShowAllRecipes();
                        break;
                    default:
                        Console.WriteLine("Bad Action. Does not exist");
                        endLoop = true;
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
