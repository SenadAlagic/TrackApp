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
    public List<ItemList> AddInBulk(List<AddToListVM> entries);
}

public class ItemListService : IItemListService
{
    private readonly IRepository<ItemList> _itemListRepository;
    private readonly IListService listService;
    private readonly IItemService itemService;
    private readonly ICategoryService categoryService;
    private readonly IPurhcaseService purchaseService;

    public ItemListService(IRepository<ItemList> itemListRepository, IListService listService, IItemService itemService,
        ICategoryService categoryService, IPurhcaseService purchaseService)
    {
        _itemListRepository = itemListRepository;
        this.listService = listService;
        this.itemService = itemService;
        this.categoryService = categoryService;
        this.purchaseService = purchaseService;
    }

    public ItemList AddItemToList(AddToListVM newEntry)
    {
        var currentWorkingList = listService.GetCurrentWorkingList();
        //check if the item already exists, if so, add quantity
        var existing = _itemListRepository.GetAll().Where(il =>
            il.ItemId == newEntry.ItemId && il.ListId == currentWorkingList.ListId &&
            il.CrossedOff == newEntry.CrossedOff).FirstOrDefault();
        if (existing == null)
        {
            var newItemList = new ItemList
            {
                Quantity = newEntry.Quantity,
                ItemId = newEntry.ItemId,
                ListId = currentWorkingList.ListId,
                DateCreated = DateTime.Now.ToUniversalTime(),
                DateModified = DateTime.Now.ToUniversalTime(),
                CrossedOff = newEntry.CrossedOff
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

        var items = itemService.GetItems();
        var categories = categoryService.GetAll();
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
        var currentList = listService.GetCurrentWorkingList();
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
        var currentList = listService.GetCurrentWorkingList();
        var itemToRestock = _itemListRepository.GetAll()
            .Where(il => il.ItemId == restock.ItemId && il.ListId == currentList.ListId && il.CrossedOff == false)
            .FirstOrDefault();
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
        purchaseService.AddPurchase(newPurchase);
        listService.UpdatePrice(currentList.ListId, restock.TotalPrice);
        return itemToRestock;
    }

    public List<ItemHistoryVM> GetItemHistory(int id)
    {
        var allLists = listService.GetAllLists();
        var allItemLists = GetAllItemList();
        var allItems = itemService.GetItems();
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
        var currentList = listService.GetCurrentWorkingList();
        var itemListToRemove = GetAllItemList()
            .Where(il => il.ItemId == itemId && il.ListId == currentList.ListId && il.CrossedOff == false)
            .FirstOrDefault();
        if (itemListToRemove != null)
            _itemListRepository.Remove(itemListToRemove);
        return itemListToRemove;
    }

    public List<ItemList> AddInBulk(List<AddToListVM> entries)
    {
        var returnList = new List<ItemList>();
        foreach (var entry in entries)
        {
            returnList.Add(AddItemToList(entry));
        }

        return returnList;
    }
}