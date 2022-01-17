using System;
using System.Collections.Generic;
using System.Linq;
using ThisRecipe.App.Concrete;
using ThisRecipe.App.Extensions;
using ThisRecipe.Domain.Entity;
using static ThisRecipe.Domain.Helpers.Helpers;

namespace ThisRecipe.App.Managers
{
    public class RecipeManager
    {
        private RecipeService _recipeService;

        public RecipeManager()
        {
            _recipeService = new RecipeService();
        }

        private string GetTitle()
        {
            Console.Write("Please insert title: ");
            return Console.ReadLine();
        }

        private string GetAuthor()
        {
            Console.Write("Please insert author: ");
            return Console.ReadLine();
        }

        private string GetDescription()
        {
            Console.Write("Please insert description: ");
            return Console.ReadLine();
        }

        private RecipeSourceType GetSourceOfRecipe()
        {
            bool isCorrect;
            Console.Write("Please select source of recipe: ");
            ShowRecipeSourceType();
            isCorrect = Enum.TryParse(Console.ReadLine(), out RecipeSourceType recipeSourceType);
            while (!isCorrect)
            {
                Console.WriteLine("Can't find source. Please enter again source");
                ShowRecipeSourceType();
                isCorrect = Enum.TryParse(Console.ReadLine(), out recipeSourceType);
            }
            return recipeSourceType;
        }

        private void ShowRecipeSourceType()
        {
            int i = 0;
            foreach (RecipeSourceType recipeSource in Enum.GetValues(typeof(RecipeSourceType)))
            {
                string recipeSourceName = recipeSource == RecipeSourceType.SocialMedia ? "Social media" : recipeSource.ToString().FirstCharToUpper();
                Console.WriteLine($" {++i}) {recipeSourceName}");
            }
        }

        private short? AddPreparationDifficulty()
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

 

     

        private List<Ingredient> ManageIngredients(List<Ingredient> editIngredients = null)
        {
            bool manageIngredients = true; //TODO rozdzielić dodawanie i edycje (początek) i wspólna logika.
            List<Ingredient> ingredients = editIngredients ?? new List<Ingredient>();

            while (manageIngredients)
            {
                Console.WriteLine("Adding ingredient");
                Console.Write("Set name of ingredient: ");
                var name = Console.ReadLine();

                Console.WriteLine("Set amount of ingredient");
                decimal.TryParse(Console.ReadLine(), out decimal amount);

                Console.WriteLine("Set unit of ingredient");
                var unit = Console.ReadLine();

                ingredients.Add(new Ingredient
                {
                    Id = ingredients.Count() + 1,
                    Name = name,
                    Amount = amount,
                    Unit = unit
                });

                Console.WriteLine("Ingredient added!\nDo you want add another ingredient?\tY/N");
                var key = Console.ReadKey();

                if (Char.ToLower(key.KeyChar) != 'n')
                {
                    manageIngredients = false;
                }
            }

            return ingredients;
        }

        private List<string> AddKitchenStuff(List<string> editThings = null)
        {
            bool manageThings = true;
            List<string> thingsToPrepare = editThings ?? new List<string>();

            while (manageThings)
            {
                Console.WriteLine("Adding ingredient");
                Console.Write("Set name of thing: ");
                thingsToPrepare.Add(Console.ReadLine());

                Console.WriteLine("Kitchen stuff added!\nDo you want add another stuff??\tY/N");
                var key = Console.ReadKey();

                if (Char.ToLower(key.KeyChar) != 'n')
                {
                    manageThings = false;
                }
            }

            return thingsToPrepare;
        }

        private List<Step> AddSteps(List<Step> editSteps = null)
        {
            bool manageSteps = true;
            List<Step> steps = editSteps ?? new List<Step>();

            while (manageSteps)
            {
                Console.WriteLine("Adding recipe steps");
                Console.Write($"Setting {steps.Count() + 1} step:");
                Console.WriteLine("Add title");
                var title = Console.ReadLine();

                Console.WriteLine("Add description");
                var description = Console.ReadLine();

                steps.Add(new Step
                {
                    Title = title,
                    Description = description,
                    NumberOfStep = steps.Count() + 1
                });

                Console.WriteLine("Step added!\nDo you want add another step??\tY/N");
                var key = Console.ReadKey();

                if (Char.ToLower(key.KeyChar) != 'n')
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
                Console.WriteLine("Please set number of servings: ");
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
                short.TryParse(Console.ReadLine(), out result);
                if(correct && result > 0)
                {
                    return result;
                }
            }
            return result;
        }

        public int AddNewRecipe()
        {

            Console.WriteLine("Please complete all fields.");

            var title = GetTitle();
            var author = GetAuthor();
            var description = GetDescription();
            var recipeSource = GetSourceOfRecipe();
            var ingredients = ManageIngredients();
            var thingsToPrepare = AddKitchenStuff();
            var steps = AddSteps();
            var preparationDifficulty = AddPreparationDifficulty();
            var numberOfServings = AddNumberOfServings();
            var preparationTime = AddPreparationTime();

            List<string> ingrediens = new List<string>();
            while (true)
            {
                var ingredient = Console.ReadLine();
                if (ingredient == "b")
                {
                    break;
                }
                else if (ingredient == "s" || ingredient == "r")
                {
                    foreach (var (el, index) in ingrediens.WithIndex())
                    {
                        Console.Write($"{index + 1}) {el}");
                    }

                    if (ingredient == "r")
                    {
                        Console.WriteLine("Select number of ingredient to remove");
                        int.TryParse(Console.ReadLine(), out int ingredientIndex);
                        ingrediens.RemoveAt(ingredientIndex - 1);
                    }
                }
                else
                {
                    ingrediens.Add(ingredient);
                }
            }
            Console.Write("Please tell how do you rate the level of difficulty from 1 to 10? ");
            short.TryParse(Console.ReadLine(), out short preparationDifficulty);

            int lastId = _recipeService.GetLastId();
            Recipe newbook = new Recipe(lastId + 1, title, author, recupeSourceType, description, ingrediens, preparationDifficulty);
            _recipeService.AddItem(newbook);

            return newbook.Id;
        }

      

        public int UpdateRecipe(int id)
        {
            var recipeToUpdate = _recipeService.Items.SingleOrDefault(x => x.Id == id);
            if (recipeToUpdate == null)
            {
                return 0;
            }
            else
            {
                Console.WriteLine("What do You want to update?");
                Console.WriteLine($"1) Author");
                Console.WriteLine($"2) Title");
                Console.WriteLine($"3) Source");
                Console.WriteLine($"4) Decription");
                Console.WriteLine($"5) Ingrediens");
                Console.WriteLine($"6) Difficulty");

                int.TryParse(Console.ReadLine(), out int number);

                switch (number)
                {
                    case 1:
                        Console.WriteLine("Plase write author");
                        recipeToUpdate.Author = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Plase write title");
                        recipeToUpdate.Title = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Plase write source");
                        ShowRecipeSourceType();
                        Enum.TryParse(Console.ReadLine(), out RecipeSourceType recupeSourceType);
                        recipeToUpdate.RecipeSourceType = recupeSourceType;
                        break;
                    case 4:
                        Console.WriteLine("Plase write description");
                        recipeToUpdate.Title = Console.ReadLine();
                        break;
                    case 5:
                        recipeToUpdate.Ingrediens = UpdateIngredients();
                        break;
                    case 6:
                        Console.WriteLine("Plase write difficulty");
                        short.TryParse(Console.ReadLine(), out short difficulty);
                        recipeToUpdate.PreparationDifficulty = difficulty;
                        break;
                    default:
                        break;
                }

                return _recipeService.UpdateItem(recipeToUpdate);
            }
        }

        private List<string> UpdateIngredients(List<string> currentIngredients = null)
        {
            List<string> ingrediens = currentIngredients ?? new List<string>();
            if (currentIngredients.Count > 0)
            {
                Console.WriteLine("Ingredients:");
                foreach (var (el, index) in ingrediens.WithIndex())
                {
                    Console.Write($"{index + 1}) {el}");
                }
            }
            Console.Write("Type 'a' to add ingredient\nType 'u' to update ingredient.\nType 'b' to finish.\nType 's' to show ingredients.\nType 'r' to remove element from list. ");


            while (true)
            {
                var selctedChoice = Console.ReadKey();
                if (selctedChoice.KeyChar == 'b')
                {
                    break;
                }
                else if (selctedChoice.KeyChar == 's' || selctedChoice.KeyChar == 'r' || selctedChoice.KeyChar == 'u')
                {
                    foreach (var (el, index) in ingrediens.WithIndex())
                    {
                        Console.Write($"{index + 1}) {el}");
                    }

                    if (selctedChoice.KeyChar == 'r')
                    {
                        Console.WriteLine("Select number of ingredient to remove");
                        int.TryParse(Console.ReadLine(), out int ingredientIndex);
                        ingrediens.RemoveAt(ingredientIndex - 1);
                    }
                    else if (selctedChoice.KeyChar == 'u')
                    {
                        Console.WriteLine("Select number of ingredient to update");
                        int.TryParse(Console.ReadLine(), out int ingredientIndex);
                        Console.WriteLine("Write name of updated ingredient");
                        ingrediens[ingredientIndex - 1] = Console.ReadLine();
                    }
                }
                else if (selctedChoice.KeyChar == 'a')
                {
                    Console.WriteLine("Enter the ingredient:");
                    var ingredient = Console.ReadLine();
                    ingrediens.Add(ingredient);
                }
                else
                {
                    Console.WriteLine("unnown command.");
                }
            }

            return ingrediens;
        }

        public void RemoveRecipe()
        {

        }
    }
}
