using System;
using TrackApp.Core;
using TrackApp.Repository;
using TrackApp.Service.ViewModels;

namespace TrackApp.Service
{
	public interface IItemService
	{
		public Item AddItem(AddItemVM item);
		public Item RemoveItem(int id);
		public List<Item> GetItems();
		public Item GetById(int id);
	}
	public class ItemService : IItemService
	{
		IRepository<Item> itemRepository;
		public ItemService(IRepository<Item> itemRepository)
		{
			this.itemRepository = itemRepository;
		}

        public Item AddItem(AddItemVM item)
        {
			var newItem = new Item()
			{
				Name = item.Name,
				Unit = item.Unit,
				CategoryId=item.CategoryId
			};
			itemRepository.Add(newItem);
			return newItem;
        }

        public Item RemoveItem(int id)
		{
			var itemToDelete=itemRepository.GetAll().Where(i => i.Id == id).FirstOrDefault();
			if(itemToDelete!=null)
				itemRepository.Remove(itemToDelete);
			return itemToDelete;
		}

        public List<Item> GetItems()
        {
			return itemRepository.GetAll().ToList();
        }

        public Item GetById(int id)
        {
			return itemRepository.GetAll().Where(i => i.Id == id).FirstOrDefault();
        }
    }
}

