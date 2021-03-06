using ThisRecipe.App.Abstract;
using ThisRecipe.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace ThisRecipe.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> Items { get; set; }

        public BaseService()
        {
            Items = new List<T>();
        }

        public int AddItem(T item)
        {
            Items.Add(item);
            return item.Id;
        }

        public List<T> GetAllItems()
        {
            return Items;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }

        public int UpdateItem(T item)
        {
            var entity = Items.FirstOrDefault(x => x.Id == item.Id);
            if(entity != null)
            {
                entity = item;
            }
            return entity.Id;
        }

        public int GetLastId()
        {
            int lastId;
            if (Items.Any())
            {
                lastId = Items.OrderBy(x => x.Id).Last().Id;
            }
            else
            {
                lastId =  0;
            }
            return lastId;
        }
    }
}
