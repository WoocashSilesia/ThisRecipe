using System;

namespace ThisRecipe
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuActionService menuActonService = new MenuActionService();
            RecipeManager bookManager = new RecipeManager();

            var mainMenu = menuActonService.GetMenuActionsByMenuName("Main");
            bool endLoop = false;
            Console.WriteLine("Welcome to the MyRecipeApp!");

            while (!endLoop)
            {
                Console.WriteLine("Please let me know what do you want to do:");
                foreach (var menu in mainMenu)
                {
                    Console.WriteLine($"{menu.Id}: {menu.Name}");
                }
                Console.WriteLine();
                var selectedOperation = Console.ReadKey();

                Console.WriteLine("\n");
                switch (selectedOperation.KeyChar)
                {
                    case '1':
                        var newId = bookManager.AddNewRecipe();
                        break;
                    case '2':
                        Console.WriteLine("Not done yet");
                        break;
                    case '3':
                        Console.WriteLine("Not done yet");
                        break;
                    case '4':
                        Console.WriteLine("Not done yet");
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
