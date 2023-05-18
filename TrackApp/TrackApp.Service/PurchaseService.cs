using System;
using TrackApp.Core;
using TrackApp.Service.ViewModels;

namespace TrackApp.Service
{
	public interface IPurhcaseService
	{
		public Purchase AddPurchase(AddPurchaseVM purchase);
		public Purchase DeletePurchase(int id);
		public List<GetPurchasesVM> GetByItemID(int itemId);

	}
	public class PurchaseService : IPurhcaseService
	{
        IItemService itemService;
        public PurchaseService(IItemService itemService)
		{
            this.itemService = itemService;
		}

        public Purchase AddPurchase(AddPurchaseVM purchase)
        {
            var newPurchase = new Purchase()
            {
                Id = InMemoryDb.Purchases.Count + 1,
                ItemId = purchase.ItemId,
                Quantity = purchase.Quantity,
                Price=purchase.Price,
                DateOfPurchase = DateTime.Now,
                IsVisible=true,
            };
            InMemoryDb.Purchases.Add(newPurchase);
            return newPurchase;
        }

        public Purchase DeletePurchase(int id)
        {
            var purchaseToDelete = InMemoryDb.Purchases.Where(p => p.Id == id).FirstOrDefault();
            if(purchaseToDelete!=null)
                purchaseToDelete.IsVisible = false;
            return purchaseToDelete;
        }

        public List<GetPurchasesVM> GetByItemID(int itemId)
        {
            var purchasesToGet= InMemoryDb.Purchases.Where(p => p.ItemId == itemId && p.IsVisible==true).ToList();
            var allItems = itemService.GetItems();
            var query = from purchase in purchasesToGet
                        join item in allItems on purchase.ItemId equals item.Id
                        select new { purchase, item.Name, item.Unit };

            var returnList = new List<GetPurchasesVM>();
            foreach (var item in query)
            {
                var newPurchase = new GetPurchasesVM()
                {
                    Id = item.purchase.Id,
                    ItemId = item.purchase.ItemId,
                    DateOfPurchase = item.purchase.DateOfPurchase,
                    Quantity = item.purchase.Quantity,
                    Price=item.purchase.Price,
                    IsVisible = item.purchase.IsVisible,
                    ItemName = item.Name,
                    Unit=item.Unit
                };
                returnList.Add(newPurchase);
            }
            return returnList;
        }
    }
}

