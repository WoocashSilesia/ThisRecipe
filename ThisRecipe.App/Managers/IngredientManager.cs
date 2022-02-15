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

        public Ingredient AddIngredient()
        {
            var name = SetName();
            var amount = SetAmount();
            var unit = SetUnit();
            var ingredient = new Ingredient(name, amount, unit);
            _ingredientService.AddItem(ingredient);
            return ingredient;
        }

        private string SetName()
        {
            Console.Write("Please insert name: ");
            return Console.ReadLine();
        }

        private decimal SetAmount()
        {
            Console.Write("Please insert amount: ");
            var isOk = decimal.TryParse(Console.ReadLine(), out decimal amount);
            while (!isOk)
            {
                Console.Write("Something goes wrong. Please insert amount: ");
                isOk = decimal.TryParse(Console.ReadLine(), out amount);
            }
            return amount;
        }

        private string SetUnit()
        {
            Console.Write("Please insert unit: ");
            return Console.ReadLine();
        }
    }
}
