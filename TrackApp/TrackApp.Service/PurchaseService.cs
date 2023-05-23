using System;
using TrackApp.Core;
using TrackApp.Repository;
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
        IRepository<Purchase> purchaseRepository;
        IItemService itemService;
        public PurchaseService(IRepository<Purchase> purchaseRepository, IItemService itemService)
		{
            this.purchaseRepository = purchaseRepository;
            this.itemService = itemService;
		}

        public Purchase AddPurchase(AddPurchaseVM purchase)
        {
            var newPurchase = new Purchase()
            {
                ItemId = purchase.ItemId,
                Quantity = purchase.Quantity,
                Price=purchase.Price,
                DateOfPurchase = DateTime.Now.ToUniversalTime(),
                IsVisible=true,
            };
            purchaseRepository.Add(newPurchase);
            return newPurchase;
        }

        public Purchase DeletePurchase(int id)
        {
            var purchaseToDelete = purchaseRepository.GetAll().Where(p => p.PurchaseId == id).FirstOrDefault();
            if(purchaseToDelete!=null)
            {
                purchaseToDelete.IsVisible = false;
                purchaseRepository.Update(purchaseToDelete);
            }
            return purchaseToDelete;
        }

        public List<GetPurchasesVM> GetByItemID(int itemId)
        {
            var purchasesToGet= purchaseRepository.GetAll().Where(p => p.ItemId == itemId && p.IsVisible==true).ToList();
            var allItems = itemService.GetItems();
            var query = from purchase in purchasesToGet
                        join item in allItems on purchase.ItemId equals item.ItemId
                        select new { purchase, item.Name, item.Unit };

            var returnList = new List<GetPurchasesVM>();
            foreach (var item in query)
            {
                var newPurchase = new GetPurchasesVM()
                {
                    Id = item.purchase.PurchaseId,
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

