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
        IItemListService itemListService;
        public ItemListController(IItemListService itemListService)
        {
            this.itemListService = itemListService;
        }

        [HttpGet]
        public ActionResult<GetItemsVM> GetItemList(int id)
        {
            var itemToReturn = itemListService.GetItemList(id);
            if (itemToReturn == null)
                return BadRequest("Nonexistant Id");
            return Ok(itemToReturn);
        }

        [HttpGet]
        public ActionResult<List<ItemList>> GetByList(int id, int numberOfResults)
        {
            var itemsToReturn = itemListService.GetByListId(id, numberOfResults);
            if (itemsToReturn == null)
                return BadRequest("Nonexistant id");
            return Ok(itemsToReturn);
        }

        [HttpPost]
        public ActionResult<ItemList> AddItemToList(AddToListVM newEntry)
        {
            var itemToAdd = itemListService.AddItemToList(newEntry);
            if (itemToAdd == null)
                return BadRequest("Error while adding");
            return Ok(itemToAdd);
        }

        [HttpPost]
        public ActionResult<ItemList> Restock([FromBody] RestockVM restock)
        {
            var itemToRestock = itemListService.RestockItems(restock);
            if (itemToRestock == null)
                return BadRequest("Nonexistant id");
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
    }
}

