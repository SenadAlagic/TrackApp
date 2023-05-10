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
		public List<ItemList> GetByListId(int id);
	}
	public class ItemListService : IItemListService
	{
		public ItemListService()
		{
		}

        public ItemList AddItemToList(AddToListVM newEntry)
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

        public List<ItemList> GetByListId(int id)
        {
			return InMemoryDb.ItemsLists.Where(il => il.ListId == id).ToList();
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

