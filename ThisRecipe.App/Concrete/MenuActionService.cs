using ThisRecipe.App.Common;
using ThisRecipe.Domain.Entity;
using System.Collections.Generic;

namespace ThisRecipe.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }

        private void Initialize()
        {
            AddItem(new MenuAction(1, "Add recipe", "Main"));
            AddItem(new MenuAction(2, "List of recipes", "Main"));
           
            AddItem(new MenuAction(3, "Show details", "Recipe"));
            AddItem(new MenuAction(4, "Edit", "Recipe"));
            AddItem(new MenuAction(5, "Remove recipe", "Recipe"));
        }

        public List<MenuAction> GetMenuActionsByMenuName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();

            foreach (var menuAction in Items)
            {
                if (menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }

            return result;
        }
    }
}
