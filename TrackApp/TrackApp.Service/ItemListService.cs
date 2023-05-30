using System.Collections;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using TrackApp.Core;
using TrackApp.Repository;
using TrackApp.Service.ViewModels;

namespace TrackApp.Service;

public interface IItemListService
{
    public ItemList GetItemList(int id);
    public ItemList AddItemToList(AddToListVM newEntry);
    public ItemList RemoveFromList(int id);
    public List<GetItemsGroupedVM> GetByListId(int id, int itemId, bool filter);
    public ItemList? RestockItems(RestockVM restock);
    public List<ItemHistoryVM> GetItemHistory(int id);
    public List<ItemList> GetAllItemList();
    public ItemList RemoveByItemId(int itemId);
    public List<ItemList> RestockInBulk(List<RestockVM> entries);
    public List<DiagramVM> GetForDiagram(int itemId, bool displayInWeeks = false);
}

public class ItemListService : IItemListService
{
    private readonly IRepository<ItemList> _itemListRepository;
    private readonly IListService _listService;
    private readonly IItemService _itemService;
    private readonly ICategoryService _categoryService;
    private readonly IPurchaseService _purchaseService;

    public ItemListService(IRepository<ItemList> itemListRepository, IListService listService, IItemService itemService,
        ICategoryService categoryService, IPurchaseService purchaseService)
    {
        _itemListRepository = itemListRepository;
        this._listService = listService;
        this._itemService = itemService;
        this._categoryService = categoryService;
        this._purchaseService = purchaseService;
    }

    public ItemList AddItemToList(AddToListVM newEntry)
    {
        var currentWorkingList = _listService.GetCurrentWorkingList();
        //check if the item already exists, if so, add quantity
        var existing = _itemListRepository.GetAll().FirstOrDefault(il =>
            il.ItemId == newEntry.ItemId && il.ListId == currentWorkingList.ListId &&
            il.CrossedOff == newEntry.CrossedOff);
        if (existing == null)
        {
            var newItemList = new ItemList
            {
                Quantity = newEntry.Quantity,
                ItemId = newEntry.ItemId,
                ListId = currentWorkingList.ListId,
                DateCreated = DateTime.Now.ToUniversalTime(),
                DateModified = DateTime.Now.ToUniversalTime(),
                CrossedOff = newEntry.CrossedOff,
                AddedBy = newEntry.AddedBy
            };
            _itemListRepository.Add(newItemList);
            return newItemList;
        }

        existing.Quantity += newEntry.Quantity;
        existing.DateModified = DateTime.Now.ToUniversalTime();
        _itemListRepository.Update(existing);
        return existing;
    }

    public List<ItemList> GetAllItemList()
    {
        return _itemListRepository.GetAll().ToList();
    }

    public List<GetItemsGroupedVM> GetByListId(int id, int itemId = 0, bool filter = false)
    {
        var itemsFromDesiredList = new List<ItemList>();
        if (itemId == 0)
            itemsFromDesiredList = _itemListRepository.GetAll().Where(il => il.ListId == id)
                .ToList();
        else
            itemsFromDesiredList = _itemListRepository.GetAll().Where(il => il.ListId == id && il.ItemId == itemId)
                .ToList();

        if (filter)
            itemsFromDesiredList = itemsFromDesiredList.Where(il => il.CrossedOff == false).ToList();

        var items = _itemService.GetItems();
        var categories = _categoryService.GetAll();
        var query2 = from item in items
            join ItemList in itemsFromDesiredList on item.ItemId equals ItemList.ItemId
            join category in categories on item.CategoryId equals category.CategoryId
            group new { item, ItemList } by category.Name
            into grouped
            select new
            {
                CategoryName = grouped.Key,
                Items = from g in grouped
                    select new
                    {
                        g.item.ItemId,
                        g.ItemList.Quantity,
                        g.item.Name,
                        g.item.Unit,
                        g.ItemList.CrossedOff
                    }
            };

        var returnList = new List<GetItemsGroupedVM>();
        foreach (var category in query2)
        {
            var newCategory = new GetItemsGroupedVM();
            newCategory.CategoryName = category.CategoryName;
            newCategory.Items = new List<GetItemsVM>();
            foreach (var item in category.Items)
            {
                var newItem = new GetItemsVM
                {
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                    Name = item.Name,
                    ItemId = item.ItemId,
                    CrossedOff = item.CrossedOff
                };
                newCategory.Items.Add(newItem);
            }

            returnList.Add(newCategory);
        }

        return returnList;
    }

    public ItemList GetItemList(int id)
    {
        return _itemListRepository.GetAll().Where(il => il.ItemListId == id).FirstOrDefault();
    }

    public ItemList RemoveFromList(int id)
    {
        var currentList = _listService.GetCurrentWorkingList();
        var itemToRemove = _itemListRepository.GetAll()
            .Where(il => il.ItemListId == id && il.ListId == currentList.ListId).FirstOrDefault();
        if (itemToRemove != null)
            _itemListRepository.Remove(itemToRemove);
        return itemToRemove;
    }

    public ItemList? RestockItems(RestockVM restock)
    {
        if (restock == null)
            return null;
        var currentList = _listService.GetCurrentWorkingList();
        var itemToRestock = _itemListRepository
            .GetAll()
            .FirstOrDefault(il =>
                il.ItemId == restock.ItemId && il.ListId == currentList.ListId && il.CrossedOff == false);
        if (itemToRestock == null)
            return null;

        itemToRestock.Quantity -= restock.Quantity;
        if (itemToRestock.Quantity <= 0)
            RemoveFromList(itemToRestock.ItemListId);
        else
            _itemListRepository.Update(itemToRestock);
        var newItem = new AddToListVM
        {
            ItemId = itemToRestock.ItemId,
            Quantity = restock.Quantity,
            //ListId = restock.ListId,
            CrossedOff = true
        };
        AddItemToList(newItem);
        var newPurchase = new AddPurchaseVM
            { ItemId = restock.ItemId, Quantity = restock.Quantity, Price = restock.TotalPrice };
        _purchaseService.AddPurchase(newPurchase);
        _listService.UpdatePrice(currentList.ListId, restock.TotalPrice);
        return itemToRestock;
    }

    public List<ItemHistoryVM> GetItemHistory(int id)
    {
        var allLists = _listService.GetAllLists();
        var allItemLists = GetAllItemList();
        var allItems = _itemService.GetItems();
        var itemHistory = allLists
            .Join(allItemLists,
                list => list.ListId,
                itemList => itemList.ListId,
                (list, itemList) => new { List = list, ItemList = itemList })
            .Join(allItems,
                joined => joined.ItemList.ItemId,
                item => item.ItemId,
                (joined, item) => new { joined.List, joined.ItemList, Item = item })
            .Where(joined => joined.Item.ItemId == id)
            .Select(joined => joined.List).Distinct().ToList();

        var returnList = new List<ItemHistoryVM>();
        foreach (var element in itemHistory)
        {
            var newEntry = new ItemHistoryVM
                { Items = GetByListId(element.ListId, id), MonthOfYear = element.MonthOfYear };
            returnList.Add(newEntry);
        }

        return returnList;
    }

    public ItemList RemoveByItemId(int itemId)
    {
        var currentList = _listService.GetCurrentWorkingList();
        var itemListToRemove = GetAllItemList()
            .FirstOrDefault(il => il.ItemId == itemId && il.ListId == currentList.ListId && il.CrossedOff == false);
        if (itemListToRemove != null)
            _itemListRepository.Remove(itemListToRemove);
        return itemListToRemove;
    }

    public List<ItemList> RestockInBulk(List<RestockVM> entries)
    {
        var returnList = new List<ItemList>();
        foreach (var entry in entries)
        {
            returnList.Add(RestockItems(entry));
        }

        return returnList;
    }

    public string DetermineGroupBy(Purchase purchase, bool filter)
    {
        if (filter)
            return (purchase.DateOfPurchase.DayOfYear / 7).ToString();
        return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(purchase.DateOfPurchase.Month);
    }
    public List<DiagramVM> GetForDiagram(int itemId, bool displayInWeeks = false)
    {
        var purchases = _purchaseService.GetByItemIdNoVM(itemId);
        purchases.Sort((x, y) => x.DateOfPurchase.CompareTo(y.DateOfPurchase));
        var pairs = new List<DiagramVM>();

        var query = from purchase in purchases
            group purchase by DetermineGroupBy(purchase,displayInWeeks)
            into groupedPurchases
            select groupedPurchases;

        //to display "Week x" instead of x and to display "March" instead of "Week March" for e.g
        var labelPrefix = "";
        if (displayInWeeks)
            labelPrefix = "Week ";
        
        foreach (var item in query)
        {
            var sum = 0;
            foreach (var entry in item)
                sum += entry.Quantity;
            var newPair = new DiagramVM()
            {
                Label = labelPrefix + item.Key,
                Value = sum
            };
            pairs.Add(newPair);
        }

        return pairs;
    }
}