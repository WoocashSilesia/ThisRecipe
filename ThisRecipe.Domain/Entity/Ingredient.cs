using ThisRecipe.Domain.Common;

namespace ThisRecipe.Domain.Entity
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Unit { get; set; }
        public Ingredient(string name, decimal amount, string unit)
        {
            Name = name;
            Amount = amount;
            Unit = unit;
        }
    }
}
