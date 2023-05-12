using System;
using TrackApp.Core;
using TrackApp.Service.ViewModels;
namespace TrackApp.Service
{
	public interface IItemListService
	{
		public ItemList GetItemList(int id);
		public ItemList AddItemToList(AddToListVM newEntry);
		public ItemList RemoveFromList(int id);
		public List<GetItemsGroupedVM> GetByListId(int id, int numberOfResults);
		public ItemList? RestockItems(RestockVM restock);
	}
	public class ItemListService : IItemListService
	{
		IListService listService;
		IItemService itemService;
		ICategoryService categoryService;
		public ItemListService(IListService listService, IItemService itemService, ICategoryService categoryService)
		{
			this.listService = listService;
			this.itemService = itemService;
			this.categoryService = categoryService;
		}

        public ItemList AddItemToList(AddToListVM newEntry)
        {
			//check if the item already exists, if so, add quantity
			var existing = InMemoryDb.ItemsLists.Where(il => il.ItemId == newEntry.ItemId).FirstOrDefault();
			if (existing == null)
			{
				var newItemList = new ItemList()
				{
					Quantity = newEntry.Quantity,
					ItemId = newEntry.ItemId,
					ListId = newEntry.ListId,
					DateCreated = DateTime.Now,
					DateModified = DateTime.Now
				};
				InMemoryDb.ItemsLists.Add(newItemList);
				return newItemList;
			}
			existing.Quantity += newEntry.Quantity;
			existing.DateModified = DateTime.Now;
			return existing;
        }

        public List<GetItemsGroupedVM> GetByListId(int id, int numberOfResults=5)
        {
			var itemsFromDesiredList= InMemoryDb.ItemsLists.Where(il => il.ListId == id && il.Quantity>0).ToList();
			var items = itemService.GetItems();
			var categories = categoryService.GetAll();
            var query2 = from item in items
                        join ItemList in itemsFromDesiredList on item.Id equals ItemList.ItemId
                        join category in categories on item.CategoryId equals category.Id
                        group new { item, ItemList } by category.Name into grouped
                        select new
                        {
                            CategoryName = grouped.Key,
                            Items = from g in grouped
                                    select new
                                    {
                                        g.item.Id,
                                        g.ItemList.Quantity,
                                        g.item.Name,
                                        g.item.Unit
                                    }
                        };

			var returnList = new List<GetItemsGroupedVM>();
			foreach(var category in query2)
			{
				var newCategory = new GetItemsGroupedVM();
				newCategory.CategoryName = category.CategoryName;
				newCategory.Items = new List<GetItemsVM>();
				foreach (var item in category.Items)
				{
					var newItem = new GetItemsVM()
					{
						Quantity = item.Quantity,
						Unit = item.Unit,
						Name = item.Name,
						ItemId = item.Id
					};
					newCategory.Items.Add(newItem);
				}
				returnList.Add(newCategory);
			}
            return returnList;
        }

        public ItemList GetItemList(int id)
        {
			return InMemoryDb.ItemsLists.Where(il => il.Id == id).FirstOrDefault();
        }

        public ItemList RemoveFromList(int id)
        {
			var itemToRemove = InMemoryDb.ItemsLists.Where(il => il.Id == id).FirstOrDefault();
			if (itemToRemove != null)
				InMemoryDb.ItemsLists.Remove(itemToRemove);
            return itemToRemove;
        }

        public ItemList? RestockItems(RestockVM restock)
        {
			var itemToRestock = InMemoryDb.ItemsLists.Where(il => il.ItemId == restock.ItemId && il.ListId==restock.ListId).FirstOrDefault();
			if (itemToRestock == null)
				return null;
			itemToRestock.Quantity -= restock.Quantity;
			if (itemToRestock.Quantity <= 0)
				itemToRestock.CrossedOff = true;
			listService.UpdatePrice(restock.ListId, restock.TotalPrice);
			return itemToRestock;
        }
    }
}

