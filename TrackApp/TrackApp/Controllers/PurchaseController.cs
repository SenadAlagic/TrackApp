using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackApp.Core;
using TrackApp.Service;
using TrackApp.Service.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrackApp.Controllers
{
    [Route("[controller]/[action]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            this._purchaseService = purchaseService;
        }

        [HttpGet]
        public ActionResult<List<Purchase>> GetByItemId(int itemId)
        {
            var purchaseToGet=_purchaseService.GetByItemId(itemId);
            if (purchaseToGet == null)
                return BadRequest("Probably a nonexistent item ID");
            return Ok(purchaseToGet);
        }
        
        [HttpGet]
        public ActionResult<List<Purchase>> GetByItemIdRaw(int itemId)
        {
            var purchaseToGet=_purchaseService.GetByItemIdNoVM(itemId);
            if (purchaseToGet == null)
                return BadRequest("Probably a nonexistent item ID");
            return Ok(purchaseToGet);
        }

        [HttpGet]
        public ActionResult<Purchase> GetPurchaseById(int purchaseId)
        {
            var purchaseToGet = _purchaseService.GetByPurchaseId(purchaseId);
            if (purchaseToGet == null)
                return BadRequest("Nonexistent ID, probably");
            return Ok(purchaseToGet);
        }

        [HttpPost]
        public ActionResult<Purchase> AddPurchase([FromBody] AddPurchaseVM purchase)
        {
            if (purchase.ItemId == 0 || purchase.Quantity == 0 || purchase.Price == 0)
                return BadRequest();
            var purchaseToAdd = _purchaseService.AddPurchase(purchase);
            if (purchaseToAdd == null)
                return BadRequest("Something went wrong while adding");
            return Ok(purchaseToAdd);
        }

        [HttpDelete]
        public ActionResult<Purchase> DeletePurchase(int id)
        {
            var purchaseToDelete = _purchaseService.DeletePurchase(id);
            if (purchaseToDelete == null)
                return BadRequest("Something went wrong while deleting");
            return purchaseToDelete;
        }
    }
}

