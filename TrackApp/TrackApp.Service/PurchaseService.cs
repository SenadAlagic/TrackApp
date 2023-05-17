using System;
using TrackApp.Core;
using TrackApp.Service.ViewModels;

namespace TrackApp.Service
{
	public interface IPurhcaseService
	{
		public Purchase AddPurchase(AddPurchaseVM purchase);
		public Purchase DeletePurchase(int id);
		public List<Purchase> GetByItemID(int itemId);

	}
	public class PurchaseService : IPurhcaseService
	{
		public PurchaseService()
		{
		}

        public Purchase AddPurchase(AddPurchaseVM purchase)
        {
            var newPurchase = new Purchase()
            {
                Id = InMemoryDb.Purchases.Count + 1,
                ItemId = purchase.ItemId,
                Quantity = purchase.Quantity,
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

        public List<Purchase> GetByItemID(int itemId)
        {
            return InMemoryDb.Purchases.Where(p => p.Id == itemId).ToList();
        }
    }
}

