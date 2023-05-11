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
		public List<GetItemsVM> GetByListId(int id);
	}
	public class ItemListService : IItemListService
	{
		public ItemListService()
		{
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

        public List<GetItemsVM> GetByListId(int id)
        {
			var itemsFromDesiredLists= InMemoryDb.ItemsLists.Where(il => il.ListId == id).ToList();
			var items = InMemoryDb.Items.ToList();
			var query = from item in items
						join itemlist in itemsFromDesiredLists on item.Id equals itemlist.ItemId
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
    }
}

