using System;
using System.Collections.Generic;
using System.Linq;
using ThisRecipe.App.Concrete;
using ThisRecipe.App.Extensions;
using ThisRecipe.Domain.Entity;

namespace ThisRecipe.App.Managers
{
    public class RecipeManager
    {
        private FullRecipeService _fullRecipeService;
        private SingleRecipeManager _singleRecipeManager;

        public RecipeManager(SingleRecipeManager singleRecipeManager)
        {
            _fullRecipeService = new FullRecipeService();
            _singleRecipeManager = singleRecipeManager;
        }

        private string SetTitle()
        {
            Console.Write("Please insert title: ");
            return Console.ReadLine();
        }

        private string SetAuthor()
        {
            Console.Write("Please insert author: ");
            return Console.ReadLine();
        }

        private string SetDescription()
        {
            Console.Write("Please insert description: ");
            return Console.ReadLine();
        }



        private short AddPreparationDifficulty()
        {
            bool correct = false;
            short result = 0;
            while (!correct)
            {
                Console.Write("Please insert preparation difficulty from 1 to 5: ");
                correct = short.TryParse(Console.ReadLine(), out result);
                if (correct && result >= 1 && result <= 5)
                {
                    return result;
                }
                else
                {
                    correct = !correct;
                }
            }
            return result;
        }

        private List<string> AddKitchenStuff()
        {
            bool manageThings = true;
            List<string> thingsToPrepare = new List<string>();

            Console.WriteLine("Please insert all kitchen stuff");
            while (manageThings)
            {
                Console.Write("\nAdd name of kitchen stuff:");
                thingsToPrepare.Add(Console.ReadLine());

                Console.WriteLine("Kitchen stuff added!\n\nDo you want add another stuff??\tY/N");
                var key = Console.ReadKey();

                if (Char.ToLower(key.KeyChar) != 'y')
                {
                    manageThings = false;
                    Console.WriteLine();
                }
            }

            return thingsToPrepare;
        }

        private List<Step> AddSteps()
        {
            bool manageSteps = true;
            List<Step> steps = new List<Step>();
            Console.WriteLine("Please insert steps in recipe");
            while (manageSteps)
            {
                Console.Write("Add title:");
                var title = Console.ReadLine();

                Console.Write("Add description:");
                var description = Console.ReadLine();

                steps.Add(new Step(steps.Count() + 1, title, description));

                Console.WriteLine($"Step {steps.Last().Title} added!\n\nDo you want add another step??\tY/N");
                var key = Console.ReadKey();

                if (Char.ToLower(key.KeyChar) != 'y')
                {
                    manageSteps = false;
                    Console.WriteLine();
                }
            }

            return steps;
        }

        private short AddNumberOfServings()
        {
            bool correct = false;
            short numberOfServings = 0;
            while (!correct)
            {
                Console.Write("Please set number of servings: ");
                correct = short.TryParse(Console.ReadLine(), out numberOfServings);
                if (correct && numberOfServings > 0)
                {
                    return numberOfServings;
                }
            }
            return numberOfServings;
        }

        private short AddPreparationTime()
        {
            bool correct = false;
            short result = 0;
            while (!correct)
            {
                Console.Write("Please set preparation time in minutes: ");
                correct = short.TryParse(Console.ReadLine(), out result);
                if (correct && result > 0)
                {
                    return result;
                }
            }
            return result;
        }

        private List<SingleRecipe> AddRecipes()
        {
            var recipes = new List<SingleRecipe>();
            bool stop = false;

            while (!stop)
            {
                //Pobierz przepis jak już istnieje
                Console.WriteLine("Choose an option:\n1)Add new recipe\n2)Add recipe from list");

                bool isParsed = int.TryParse(Console.ReadLine(), out int addOption);
                while (!isParsed || addOption < 1 || addOption > 2)
                {
                    Console.WriteLine("Wrong number, try again");
                    isParsed = int.TryParse(Console.ReadLine(), out addOption);
                }

                switch (addOption)
                {
                    case 1:
                        recipes.Add(_singleRecipeManager.CreateSingleRecipe());
                        break;
                    case 2:
                        recipes.Add(_singleRecipeManager.AddExistedSingleRecipe(recipes));
                        break;
                }

                while (isParsed)
                {
                    Console.WriteLine("1) Add another recipe");
                    Console.WriteLine("2) Exit");
                    isParsed = int.TryParse(Console.ReadLine(), out int continueOption);
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

            return recipes;
        }

        public int AddNewRecipe()
        {
            Console.WriteLine("\n-----Adding a recipe-----");
            Console.WriteLine("Please complete all fields.\n");
            Console.WriteLine("-----TITLE-----");
            var title = SetTitle();
            Console.WriteLine("-----AUTHOR-----");
            var author = SetAuthor();
            Console.WriteLine("-----DESCRIPTION-----");
            var description = SetDescription();
            Console.WriteLine("-----KITCHEN STUFF-----");
            var kitchenStuff = AddKitchenStuff();
            Console.WriteLine("-----STEPS-----");
            var steps = AddSteps();
            Console.WriteLine("-----PREPARATION DIFFICULTY-----");
            var preparationDifficulty = AddPreparationDifficulty();
            Console.WriteLine("-----NUMBER OF SERVINGS-----");
            var numberOfServings = AddNumberOfServings();
            Console.WriteLine("-----PREPARATION TIME-----");
            var preparationTime = AddPreparationTime();
            Console.WriteLine("-----RECIPES-----");
            var singleRecipes = AddRecipes();

            var fullRecpie = new FullRecipe(
                _fullRecipeService.GetLastId(),
                title,
                author,
                description,
                kitchenStuff,
                steps,
                preparationDifficulty,
                numberOfServings,
                preparationTime,
                singleRecipes
                );

            return _fullRecipeService.AddItem(fullRecpie);
        }

        public void ShowAllRecipes()
        {
            Console.WriteLine("\n-----LIST OF RECIPES----");
            var recipes = _fullRecipeService.GetAllItems();
            foreach (var (recipe, index) in recipes.WithIndex())
            {
                Console.WriteLine($"{index + 1}) {recipe.Title}");
            }
        }

        public int EditRecipe()
        {
            bool isOk = false;
            int fieldNumber = 1;
            Console.WriteLine("\n-----EDIT RECIPE----");
            var recipe = GetRecipeFromList();
            while (!isOk && fieldNumber > 0 && fieldNumber < 7)
            {
                Console.WriteLine("What do You want to edit?");
                ShowAllEditableFields();
                isOk = int.TryParse(Console.ReadLine(), out fieldNumber);
            }

            switch (fieldNumber)
            {
                case 1:
                    recipe.Title= SetTitle();
                    break;
                case 2:
                    recipe.Author = SetAuthor();
                    break;
                case 3:
                    recipe.Description = SetDescription();
                    break;
                case 4:
                    recipe.PreparationDifficulty = AddPreparationDifficulty();
                    break;
                case 5:
                    recipe.NumberOfServings = AddNumberOfServings();
                    break;
                case 6:
                    recipe.PreparationTime = AddPreparationTime();
                    break;
                default:
                    break;
            }

            return _fullRecipeService.UpdateItem(recipe);
        }

        private FullRecipe GetRecipeFromList()
        {
            var recipes = _fullRecipeService.GetAllItems();
            bool isOk = false;

            while (!isOk)
            {
                Console.WriteLine("Select recipe to edit");
                foreach (var (recipe, index) in recipes.WithIndex())
                {
                    Console.WriteLine($"{index + 1}) {recipe.Title}");
                }

                isOk = int.TryParse(Console.ReadLine(), out int recipeNumber);

                if (recipes.ElementAtOrDefault(recipeNumber - 1) != null)
                {
                    return recipes.ElementAt(recipeNumber - 1);
                }
                else
                {
                    Console.WriteLine("Something goes wrong. Choose again.");
                    isOk = false;
                }
            }

            return null;
        }

        private void ShowAllEditableFields()
        {
            Console.WriteLine("1) Title");
            Console.WriteLine("2) Author");
            Console.WriteLine("3) Description");
            Console.WriteLine("4) Preparation difficulty");
            Console.WriteLine("5) Number of servings");
            Console.WriteLine("6) Preparation time");
        }

        public void DeleteRecipe()
        {
            bool isOk = false;
            Console.WriteLine("\n-----DELETE RECIPE----");
            var recipe = GetRecipeFromList();
            _fullRecipeService.RemoveItem(recipe);

            Console.WriteLine("Recipe removed.");
        }
    }
}
