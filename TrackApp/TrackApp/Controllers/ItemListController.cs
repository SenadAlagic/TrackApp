using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackApp.Service;
using TrackApp.Service.ViewModels;
using TrackApp.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackApp.Controllers
{
    [Route("[controller]/[action]")]
    public class ItemListController : ControllerBase
    {
        private IItemListService itemListService;

        public ItemListController(IItemListService itemListService)
        {
            this.itemListService = itemListService;
        }

        [HttpGet]
        public ActionResult<GetItemsVM> GetItemList(int id)
        {
            var itemToReturn = itemListService.GetItemList(id);
            if (itemToReturn == null)
                return BadRequest("Nonexistent Id");
            return Ok(itemToReturn);
        }

        [HttpGet]
        public ActionResult<List<ItemList>> GetByList(int id, int itemId, bool filter)
        {
            if (id == 0)
                return BadRequest();
            var itemsToReturn = itemListService.GetByListId(id, itemId, filter);
            if (itemsToReturn == null)
                return BadRequest("Nonexistent id");
            return Ok(itemsToReturn);
        }

        [HttpGet]
        public ActionResult<List<List>> GetItemHistory(int itemId)
        {
            var listToReturn = itemListService.GetItemHistory(itemId);
            if (listToReturn == null)
                return BadRequest("Something unexpected happened");
            return Ok(listToReturn);
        }

        [HttpPost]
        public ActionResult<ItemList> AddItemToList(AddToListVM newEntry)
        {
            if (newEntry.ItemId == 0 || newEntry.Quantity == 0)
                return BadRequest();
            var itemToAdd = itemListService.AddItemToList(newEntry);
            if (itemToAdd == null)
                return BadRequest("Error while adding");
            return Ok(itemToAdd);
        }

        [HttpPost]
        public ActionResult<List<ItemList>> AddItemsInBulk(List<AddToListVM> entries)
        {
            if (entries.Count == 0)
                return BadRequest();
            return itemListService.AddInBulk(entries);
        }

        [HttpPost]
        public ActionResult<ItemList> Restock([FromBody] RestockVM restock)
        {
            if (restock.ItemId == 0 || restock.Quantity == 0 || restock.TotalPrice == 0)
                return BadRequest();
            var itemToRestock = itemListService.RestockItems(restock);
            if (itemToRestock == null)
                return BadRequest("Nonexistent id");
            return Ok(itemToRestock);
        }

        [HttpDelete]
        public ActionResult<ItemList> RemoveItemFromList(int id)
        {
            var itemToRemove = itemListService.RemoveFromList(id);
            if (itemToRemove == null)
                return BadRequest("Error while deleting");
            return Ok(itemToRemove);
        }

        [HttpDelete]
        public ActionResult<ItemList> RemoveFromListByItemId(int itemId)
        {
            var itemListToRemove = itemListService.RemoveByItemId(itemId);
            if (itemListToRemove == null)
                return BadRequest("Error while removing");
            return Ok(itemListToRemove);
        }
    }
}