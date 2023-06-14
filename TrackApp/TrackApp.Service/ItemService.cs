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
        public Item AddImageLink(int id, string imageLink);
    }

    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly ICategoryService _categoryService;

        public ItemService(IRepository<Item> itemRepository, ICategoryService categoryService)
        {
            this._itemRepository = itemRepository;
            this._categoryService = categoryService;
        }

        public Item AddItem(AddItemVM item)
        {
            var newItem = new Item()
            {
                Name = item.Name,
                Unit = item.Unit,
                CategoryId = item.CategoryId
            };
            _itemRepository.Add(newItem);
            return newItem;
        }

        public Item RemoveItem(int id)
        {
            var itemToDelete = _itemRepository.GetAll().FirstOrDefault(i => i.ItemId == id);
            if (itemToDelete != null)
                _itemRepository.Remove(itemToDelete);
            return itemToDelete;
        }

        public List<Item> GetItems()
        {
            return _itemRepository.GetAll().ToList();
        }

        public Item GetById(int id)
        {
            return _itemRepository.GetAll().Where(i => i.ItemId == id).FirstOrDefault();
        }

        public List<ItemsByCategoryVM> GetItemsByCategory()
        {
            var categories = _categoryService.GetAll();
            var items = GetItems();
            var query = from category in categories
                join item in items on category.CategoryId equals item.CategoryId
                group item by category.Name;

            var result = new List<ItemsByCategoryVM>();
            foreach (var group in query)
            {
                var newEntry = new ItemsByCategoryVM();
                newEntry.CategoryName = group.Key;
                newEntry.Items = group.ToList();
                result.Add(newEntry);
            }

            return result;
        }

        public Item AddImageLink(int id, string imageLink)
        {
            var itemToGet = GetById(id);
            if (itemToGet == null)
                return null;
            itemToGet.ImageLink = imageLink;
            _itemRepository.Update(itemToGet);
            return itemToGet;
        }
    }
}