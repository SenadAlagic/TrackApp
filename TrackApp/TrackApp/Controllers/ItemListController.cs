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
        private readonly IItemListService _itemListService;

        public ItemListController(IItemListService itemListService)
        {
            this._itemListService = itemListService;
        }

        [HttpGet]
        public ActionResult<GetItemsVM> GetItemList(int id)
        {
            var itemToReturn = _itemListService.GetItemList(id);
            if (itemToReturn == null)
                return BadRequest("Nonexistent Id");
            return Ok(itemToReturn);
        }

        [HttpGet]
        public ActionResult<List<ItemList>> GetByList(int id, int itemId, bool filter)
        {
            if (id == 0)
                return BadRequest();
            var itemsToReturn = _itemListService.GetByListId(id, itemId, filter);
            if (itemsToReturn == null)
                return BadRequest("Nonexistent id");
            return Ok(itemsToReturn);
        }

        [HttpGet]
        public ActionResult<List<List>> GetItemHistory(int itemId)
        {
            var listToReturn = _itemListService.GetItemHistory(itemId);
            if (listToReturn == null)
                return BadRequest("Something unexpected happened");
            return Ok(listToReturn);
        }

        [HttpGet]
        public ActionResult<List<DiagramVM>> GetForDiagram(int itemId, bool displayInWeeks)
        {
            var listToReturn = _itemListService.GetForDiagram(itemId, displayInWeeks);
            if (listToReturn == null)
                return BadRequest("An error occured, check the itemId");
            return Ok(listToReturn);
        }

        [HttpGet]
        public ActionResult<List<MultipleDiagramVM>> GetForDiagramMultiple(int itemId)
        {
            return Ok(_itemListService.GetForDiagramMultiple(itemId));
        }
        [HttpPost]
        public ActionResult<ItemList> AddItemToList([FromBody] AddToListVM newEntry)
        {
            if (newEntry.ItemId == 0 || newEntry.Quantity == 0)
                return BadRequest();
            var itemToAdd = _itemListService.AddItemToList(newEntry);
            if (itemToAdd == null)
                return BadRequest("Error while adding");
            return Ok(itemToAdd);
        }

        [HttpPost]
        public ActionResult<List<ItemList>> RestockInBulk([FromBody] List<RestockVM> entries)
        {
            if (entries.Count == 0)
                return BadRequest();
            return _itemListService.RestockInBulk(entries);
        }

        [HttpPost]
        public ActionResult<ItemList> Restock([FromBody] RestockVM restock)
        {
            if (restock.ItemId == 0 || restock.Quantity == 0 || restock.TotalPrice == 0)
                return BadRequest();
            var itemToRestock = _itemListService.RestockItems(restock);
            if (itemToRestock == null)
                return BadRequest("Nonexistent id");
            return Ok(itemToRestock);
        }

        [HttpDelete]
        public ActionResult<ItemList> RemoveItemFromList(int id)
        {
            var itemToRemove = _itemListService.RemoveFromList(id);
            if (itemToRemove == null)
                return BadRequest("Error while deleting");
            return Ok(itemToRemove);
        }

        [HttpDelete]
        public ActionResult<ItemList> RemoveFromListByItemId(int itemId)
        {
            var itemListToRemove = _itemListService.RemoveByItemId(itemId);
            if (itemListToRemove == null)
                return BadRequest("Error while removing");
            return Ok(itemListToRemove);
        }
    }
}