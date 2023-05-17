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
        IPurhcaseService purchaseService;
        public PurchaseController(IPurhcaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        [HttpGet]
        public ActionResult<List<Purchase>> GetByItemId(int itemId)
        {
            var purchaseToGet=purchaseService.GetByItemID(itemId);
            if (purchaseToGet == null)
                return BadRequest("Probably a nonexistant item ID");
            return Ok(purchaseToGet);
        }

        [HttpPost]
        public ActionResult<Purchase> AddPurchase([FromBody] AddPurchaseVM purchase)
        {
            var purchaseToAdd = purchaseService.AddPurchase(purchase);
            if (purchaseToAdd == null)
                return BadRequest("Something went wrong while adding");
            return Ok(purchaseToAdd);
        }

        [HttpDelete]
        public ActionResult<Purchase> DeletePurchase(int id)
        {
            var purchaseToDelete = purchaseService.DeletePurchase(id);
            if (purchaseToDelete == null)
                return BadRequest("Something went wrong while deleting");
            return purchaseToDelete;
        }
    }
}

