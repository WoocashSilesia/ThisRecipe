using System.Collections.Generic;

namespace ThisRecipe.App.Abstract
{
    public interface IService<T>
    {
        List<T> Items { get; set; }
        List<T> GetAllItems();
        int AddItem(T item);
        int UpdateItem(T item);
        void RemoveItem(T item);
    }
}
