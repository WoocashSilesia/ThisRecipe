using System;
using System.Collections.Generic;
using System.Linq;
using ThisRecipe.App.Concrete;
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
                Console.WriteLine("\nAdd name of kitchen stuff:");
                thingsToPrepare.Add(Console.ReadLine());

                Console.WriteLine("Kitchen stuff added!\nDo you want add another stuff??\tY/N");
                var key = Console.ReadKey();

                if (Char.ToLower(key.KeyChar) != 'y')
                {
                    manageThings = false;
                }
            }

            return thingsToPrepare;
        }

        private List<Step> AddSteps()
        {
            bool manageSteps = true;
            List<Step> steps = new List<Step>();
            Console.WriteLine("\nPlease insert steps in recipe");
            while (manageSteps)
            {
                Console.WriteLine("Add title");
                var title = Console.ReadLine();

                Console.WriteLine("Add description");
                var description = Console.ReadLine();

                steps.Add(new Step(steps.Count() + 1, title, description));

                Console.WriteLine($"Step {steps.Last().Title} added!\nDo you want add another step??\tY/N");
                var key = Console.ReadKey();

                if (Char.ToLower(key.KeyChar) != 'y')
                {
                    manageSteps = false;
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
                Console.WriteLine("\nPlease set number of servings: ");
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
                Console.WriteLine("Please set preparation time in minutes: ");
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
                while (!isParsed || addOption < 1  || addOption > 2)
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
                        recipes.Add(_singleRecipeManager.AddExistedSingleRecipe());
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
            Console.WriteLine("\n\nPlease complete all fields.");
            var title = SetTitle();
            var author = SetAuthor();
            var description = SetDescription();
            var kitchenStuff = AddKitchenStuff();
            var steps = AddSteps();
            var preparationDifficulty = AddPreparationDifficulty();
            var numberOfServings = AddNumberOfServings();
            var preparationTime = AddPreparationTime();
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

        //public int UpdateRecipe(int id)
        //{
        //    var recipeToUpdate = _fullRecipeService.Items.SingleOrDefault(x => x.Id == id);
        //    if (recipeToUpdate == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        Console.WriteLine("What do You want to update?");
        //        Console.WriteLine($"1) Author");
        //        Console.WriteLine($"2) Title");
        //        Console.WriteLine($"3) Source");
        //        Console.WriteLine($"4) Decription");
        //        Console.WriteLine($"5) Ingrediens");
        //        Console.WriteLine($"6) Difficulty");

        //        int.TryParse(Console.ReadLine(), out int number);

        //        switch (number)
        //        {
        //            case 1:
        //                Console.WriteLine("Plase write author");
        //                recipeToUpdate.Author = Console.ReadLine();
        //                break;
        //            case 2:
        //                Console.WriteLine("Plase write title");
        //                recipeToUpdate.Title = Console.ReadLine();
        //                break;
        //            case 3:
        //                Console.WriteLine("Plase write source");
        //                ShowRecipeSource();
        //                Enum.TryParse(Console.ReadLine(), out RecipeSource recupeSourceType);
        //                recipeToUpdate.RecipeSource = recupeSourceType;
        //                break;
        //            case 4:
        //                Console.WriteLine("Plase write description");
        //                recipeToUpdate.Title = Console.ReadLine();
        //                break;
        //            case 5:
        //                recipeToUpdate.Recpies = UpdateIngredients();
        //                break;
        //            case 6:
        //                Console.WriteLine("Plase write difficulty");
        //                short.TryParse(Console.ReadLine(), out short difficulty);
        //                recipeToUpdate.PreparationDifficulty = difficulty;
        //                break;
        //            default:
        //                break;
        //        }

        //        return _fullRecipeService.UpdateItem(recipeToUpdate);
        //    }
        //}

        //private List<string> UpdateIngredients(List<string> currentIngredients = null)
        //{
        //    List<string> ingrediens = currentIngredients ?? new List<string>();
        //    if (currentIngredients.Count > 0)
        //    {
        //        Console.WriteLine("Ingredients:");
        //        foreach (var (el, index) in ingrediens.WithIndex())
        //        {
        //            Console.Write($"{index + 1}) {el}");
        //        }
        //    }
        //    Console.Write("Type 'a' to add ingredient\nType 'u' to update ingredient.\nType 'b' to finish.\nType 's' to show ingredients.\nType 'r' to remove element from list. ");


        //    while (true)
        //    {
        //        var selctedChoice = Console.ReadKey();
        //        if (selctedChoice.KeyChar == 'b')
        //        {
        //            break;
        //        }
        //        else if (selctedChoice.KeyChar == 's' || selctedChoice.KeyChar == 'r' || selctedChoice.KeyChar == 'u')
        //        {
        //            foreach (var (el, index) in ingrediens.WithIndex())
        //            {
        //                Console.Write($"{index + 1}) {el}");
        //            }

        //            if (selctedChoice.KeyChar == 'r')
        //            {
        //                Console.WriteLine("Select number of ingredient to remove");
        //                int.TryParse(Console.ReadLine(), out int ingredientIndex);
        //                ingrediens.RemoveAt(ingredientIndex - 1);
        //            }
        //            else if (selctedChoice.KeyChar == 'u')
        //            {
        //                Console.WriteLine("Select number of ingredient to update");
        //                int.TryParse(Console.ReadLine(), out int ingredientIndex);
        //                Console.WriteLine("Write name of updated ingredient");
        //                ingrediens[ingredientIndex - 1] = Console.ReadLine();
        //            }
        //        }
        //        else if (selctedChoice.KeyChar == 'a')
        //        {
        //            Console.WriteLine("Enter the ingredient:");
        //            var ingredient = Console.ReadLine();
        //            ingrediens.Add(ingredient);
        //        }
        //        else
        //        {
        //            Console.WriteLine("unnown command.");
        //        }
        //    }

        //    return ingrediens;
        //}

        public void RemoveRecipe()
        {

        }
    }
}
