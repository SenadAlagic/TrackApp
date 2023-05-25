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
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            this._itemService = itemService;
        }

        [HttpGet]
        public ActionResult<List<Item>> GetItems()
        {
            return Ok(_itemService.GetItems());
        }

        [HttpGet]
        public ActionResult<Item> GetById(int id)
        {
            var itemToGet = _itemService.GetById(id);
            if (itemToGet == null)
                return BadRequest("Nonexistent id");
            return itemToGet;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, List<Item>>> GetItemsByCategory()
        {
            return Ok(_itemService.GetItemsByCategory());
        }

        [HttpPost]
        public ActionResult<Item> AddItem([FromBody] AddItemVM newItem)
        {
            if (string.IsNullOrWhiteSpace(newItem.Name))
                return BadRequest("Invalid name");
            if (string.IsNullOrWhiteSpace(newItem.Unit))
                return BadRequest("Invalid unit");
            if (newItem.CategoryId == 0)
                return BadRequest("Invalid category");
            return Ok(_itemService.AddItem(newItem));
        }

        [HttpDelete]
        public ActionResult<Item> RemoveItem(int id)
        {
            var itemToRemove = _itemService.RemoveItem(id);
            if (itemToRemove == null)
                return BadRequest("Nonexistent id");
            return Ok(itemToRemove);
        }
    }
}

