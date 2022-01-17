using ThisRecipe.Domain.Common;

namespace ThisRecipe.Domain.Entity
{
    public class Step : BaseEntity
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Step(int order, string title)
        {
            Order = order;
            Title = title;
        }
    }
}
