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
        public ActionResult<ItemList> GetItemList(int id)
        {
            var itemToReturn = itemListService.GetItemList(id);
            if (itemToReturn == null)
                return BadRequest("Nepostojeci Id");
            return Ok(itemToReturn);
        }

        [HttpGet]
        public ActionResult<List<ItemList>> GetByItemList(int id)
        {
            var itemsToReturn = itemListService.GetByListId(id);
            if (itemsToReturn == null)
                return BadRequest("Nepostojeci id");
            return Ok(itemsToReturn);
        }

        [HttpPost]
        public ActionResult<ItemList> AddItemToList(AddToListVM newEntry)
        {
            var itemToAdd=itemListService.AddItemToList(newEntry);
            if (itemToAdd == null)
                return BadRequest("Greska pri dodavanju");
            return Ok(itemToAdd);
        }

        [HttpDelete]
        public ActionResult<ItemList> RemoveItemFromList(int id)
        {
            var itemToRemove = itemListService.RemoveFromList(id);
            if (itemToRemove == null)
                return BadRequest("Greska pri uklanjanju");
            return Ok(itemToRemove);
        }
    }
}

