using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackApp.Core;
using TrackApp.Service;
using TrackApp.Service.ViewModels;

namespace TrackApp.Controllers
{
    [Route("[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        IItemService itemService;
        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpGet]
        public ActionResult<List<Item>> GetItems()
        {
            return Ok(itemService.GetItems());
        }

        [HttpGet]
        public ActionResult<Item> GetById(int id)
        {
            var itemToGet = itemService.GetById(id);
            if (itemToGet == null)
                return BadRequest("Nonexistant id");
            return itemToGet;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, List<Item>>> GetItemsByCategory()
        {
            return Ok(itemService.GetItemsByCategory());
        }

        [HttpPost]
        public ActionResult<Item> AddItem([FromBody] AddItemVM newItem)
        {
            return Ok(itemService.AddItem(newItem));
        }

        [HttpDelete]
        public ActionResult<Item> RemoveItem(int id)
        {
            var itemToRemove = itemService.RemoveItem(id);
            if (itemToRemove == null)
                return BadRequest("Nonexistant id");
            return Ok(itemToRemove);
        }
    }
}

