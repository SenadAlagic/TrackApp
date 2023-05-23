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
		public List<ItemsByCategoryVM> GetItemsByCategory();
		public Item GetById(int id);
	}
	public class ItemService : IItemService
	{
		IRepository<Item> itemRepository;
		ICategoryService categoryService;
		public ItemService(IRepository<Item> itemRepository, ICategoryService categoryService)
		{
			this.itemRepository = itemRepository;
			this.categoryService = categoryService;
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
			var itemToDelete=itemRepository.GetAll().Where(i => i.ItemId == id).FirstOrDefault();
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
			return itemRepository.GetAll().Where(i => i.ItemId == id).FirstOrDefault();
        }

        public List<ItemsByCategoryVM> GetItemsByCategory()
        {
			var categories = categoryService.GetAll();
			var items = GetItems();
			var query = from category in categories
						join item in items on category.CategoryId equals item.CategoryId
						group item by category.Name;

			var result = new List<ItemsByCategoryVM>();
			foreach(var group in query)
			{
				var newEntry = new ItemsByCategoryVM();
				newEntry.CategoryName = group.Key;
				newEntry.Items = group.ToList();
				result.Add(newEntry);
			}
			return result;
        }
    }
}

