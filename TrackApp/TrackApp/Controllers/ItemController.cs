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

        [HttpPost]
        public ActionResult<Item> AddToList([FromBody] AddItemVM newItem)
        {
            return Ok(itemService.AddToList(newItem));
        }

        [HttpDelete]
        public ActionResult<Item> RemoveFromList(int id)
        {
            var itemToRemove = itemService.RemoveFromList(id);
            if (itemToRemove == null)
                return BadRequest("Nonexistant id");
            return Ok(itemToRemove);
        }
    }
}

