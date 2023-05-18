﻿using System;
using TrackApp.Core;
using TrackApp.Service.ViewModels;
namespace TrackApp.Service
{
	public interface IItemListService
	{
		public ItemList GetItemList(int id);
		public ItemList AddItemToList(AddToListVM newEntry);
		public ItemList RemoveFromList(int id);
		public List<GetItemsGroupedVM> GetByListId(int id, int numberOfResults, int itemId);
		public ItemList? RestockItems(RestockVM restock);
		public List<ItemHistoryVM> GetItemHistory(int id);
		public List<ItemList> GetAllItemList();
    }
	public class ItemListService : IItemListService
	{
		IListService listService;
		IItemService itemService;
		ICategoryService categoryService;
		IPurhcaseService purchaseService;
		public ItemListService(IListService listService, IItemService itemService, ICategoryService categoryService, IPurhcaseService purchaseService)
		{
			this.listService = listService;
			this.itemService = itemService;
			this.categoryService = categoryService;
			this.purchaseService = purchaseService;
		}

        public ItemList AddItemToList(AddToListVM newEntry)
        {
			//check if the item already exists, if so, add quantity
			var existing = InMemoryDb.ItemsLists.Where(il => il.ItemId == newEntry.ItemId && il.ListId==newEntry.ListId && il.CrossedOff==newEntry.CrossedOff).FirstOrDefault();
			if (existing == null)
			{
				var newItemList = new ItemList()
				{
					Id=InMemoryDb.ItemsLists.Count+1,
					Quantity = newEntry.Quantity,
					ItemId = newEntry.ItemId,
					ListId = newEntry.ListId,
					DateCreated = DateTime.Now,
					DateModified = DateTime.Now,
					CrossedOff = newEntry.CrossedOff,
				};
				InMemoryDb.ItemsLists.Add(newItemList);
				return newItemList;
			}
			existing.Quantity += newEntry.Quantity;
			existing.DateModified = DateTime.Now;
			return existing;
        }

        public List<ItemList> GetAllItemList()
        {
			return InMemoryDb.ItemsLists.ToList();
        }

        public List<GetItemsGroupedVM> GetByListId(int id, int numberOfResults=100, int itemId=0)
        {
			var itemsFromDesiredList = new List<ItemList>();
			if(itemId==0)
				itemsFromDesiredList= InMemoryDb.ItemsLists.Where(il => il.ListId == id).ToList();
			else
                itemsFromDesiredList = InMemoryDb.ItemsLists.Where(il => il.ListId == id && il.ItemId==itemId).ToList();

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
                                        g.item.Unit,
										g.ItemList.CrossedOff
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
						ItemId = item.Id,
						CrossedOff=item.CrossedOff
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
			var itemToRestock = InMemoryDb.ItemsLists.Where(il => il.ItemId == restock.ItemId && il.ListId==restock.ListId && il.CrossedOff==false).FirstOrDefault();
			if (itemToRestock == null)
				return null;

			itemToRestock.Quantity -= restock.Quantity;
			if (itemToRestock.Quantity <= 0)
				RemoveFromList(itemToRestock.Id);
			var newItem = new AddToListVM()
			{
				ItemId = itemToRestock.ItemId,
				Quantity = restock.Quantity,
				ListId = restock.ListId,
				CrossedOff=true,
			};
			AddItemToList(newItem);
			var newPurchase = new AddPurchaseVM() { ItemId = restock.ItemId, Quantity = restock.Quantity, Price=restock.TotalPrice };
			purchaseService.AddPurchase(newPurchase);
			listService.UpdatePrice(restock.ListId, restock.TotalPrice);
			return itemToRestock;
        }

        public List<ItemHistoryVM> GetItemHistory(int id)
        {
            var allLists = listService.GetAllLists();
            var allItemLists = GetAllItemList();
            var allItems = itemService.GetItems();
            var itemHistory = allLists
                .Join(allItemLists,
                    list => list.Id,
                    itemList => itemList.ListId,
                    (list, itemList) => new { List = list, ItemList = itemList })
                .Join(allItems,
                    joined => joined.ItemList.ItemId,
                    item => item.Id,
                    (joined, item) => new { List = joined.List, ItemList = joined.ItemList, Item = item })
                .Where(joined => joined.Item.Id == id)
                .Select(joined => joined.List).Distinct().ToList();

			var returnList = new List<ItemHistoryVM>();
			foreach (var element in itemHistory)
			{
				var newEntry = new ItemHistoryVM() { Items = GetByListId(element.Id, 0, id), MonthOfYear = element.MonthOfYear };
				returnList.Add(newEntry);
			}
            return returnList;
        }
    }
}

