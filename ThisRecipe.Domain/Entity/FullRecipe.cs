using System.Collections.Generic;
using ThisRecipe.Domain.Common;

namespace ThisRecipe.Domain.Entity
{
    public class FullRecipe : BaseEntity
    {
        public string Title { get; set; } // Dod
        public string Author { get; set; } // Dod
        public string Description { get; set; } //Dod
        public List<SingleRecipe> Recpies { get; set; }
        public List<Step> Steps { get; set; } // Dod
        public List<string> KitchenStuff { get; set; } // Dod
        public short PreparationDifficulty { get; set; } // Dod
        public short NumberOfServings { get; set; } // Dod
        public short PreparationTime { get; set; } // Dod

        public FullRecipe(
            int id,
            string title,
            string author,
            string descripton,
            List<string> kitchenStuff,
            List<Step> steps,
            short preparationDifficulty,
            short numberOfServings,
            short preparationTime,
            List<SingleRecipe> recpies)
        {
            Id = id;
            Title = title;
            Author = author;
            Description = descripton;
            KitchenStuff = kitchenStuff;
            Steps = steps;
            NumberOfServings = numberOfServings;
            PreparationTime = preparationTime;
            PreparationDifficulty = preparationDifficulty;
            Recpies = recpies;
        }
    }
}
