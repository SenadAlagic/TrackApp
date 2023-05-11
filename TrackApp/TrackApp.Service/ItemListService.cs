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
		public List<GetItemsVM> GetByListId(int id, int numberOfResults);
		public ItemList? RestockItems(RestockVM restock);
	}
	public class ItemListService : IItemListService
	{
		IListService listService;
		public ItemListService(IListService listService)
		{
			this.listService = listService;
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

        public List<GetItemsVM> GetByListId(int id, int numberOfResults=5)
        {
			var itemsFromDesiredList= InMemoryDb.ItemsLists.Where(il => il.ListId == id && il.Quantity>0).ToList();
			var items = InMemoryDb.Items.ToList();
			var query = from item in items
						join itemlist in itemsFromDesiredList on item.Id equals itemlist.ItemId
						select new
						{
							Quantity = itemlist.Quantity,
							Name = item.Name,
							Unit = item.Unit
						};
			var returnList = new List<GetItemsVM>(); 
			foreach(var item in query)
			{
				var newItem = new GetItemsVM()
				{
					Name = item.Name,
					Quantity = item.Quantity,
					Unit = item.Unit
				};
				returnList.Add(newItem);
				if (returnList.Count == numberOfResults)
					return returnList;
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

