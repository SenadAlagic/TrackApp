using System;
using TrackApp.Core;
using TrackApp.Service.ViewModels;

namespace TrackApp.Service
{
	public interface IItemService
	{
		public Item AddItem(AddItemVM item);
		public Item RemoveItem(int id);
		public List<Item> GetItems();
	}
	public class ItemService : IItemService
	{
		public ItemService()
		{
		}

        public Item AddItem(AddItemVM item)
        {
			var newItem = new Item()
			{
				Id = InMemoryDb.Items.Count+1,
				Name = item.Name,
				Unit = item.Unit,
				CategoryId=item.CategoryId
			};
			InMemoryDb.Items.Add(newItem);
			return newItem;
        }

        public Item RemoveItem(int id)
		{
			var itemToDelete=InMemoryDb.Items.Where(i => i.Id == id).FirstOrDefault();
			if(itemToDelete!=null)
				InMemoryDb.Items.Remove(itemToDelete);
			return itemToDelete;
		}

        public List<Item> GetItems()
        {
			return InMemoryDb.Items;
        }
    }
}

