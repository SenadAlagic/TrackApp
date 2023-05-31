using System;
using TrackApp.Core;
using TrackApp.Repository;
using TrackApp.Service.ViewModels;

namespace TrackApp.Service
{
    public interface IPurchaseService
    {
        public Purchase AddPurchase(AddPurchaseVM purchase);
        public Purchase DeletePurchase(int id);
        public List<GetPurchasesVM> GetByItemId(int itemId);
        public List<Purchase> GetByItemIdNoVM(int itemId);
    }

    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IItemService _itemService;

        public PurchaseService(IRepository<Purchase> purchaseRepository, IItemService itemService)
        {
            this._purchaseRepository = purchaseRepository;
            this._itemService = itemService;
        }

        public Purchase AddPurchase(AddPurchaseVM purchase)
        {
            var newPurchase = new Purchase()
            {
                ItemId = purchase.ItemId,
                Quantity = purchase.Quantity,
                Price = purchase.Price,
                DateOfPurchase = DateTime.Now.ToUniversalTime(),
                PurchasedBy = purchase.PurchasedBy,
                IsVisible = true,
            };
            _purchaseRepository.Add(newPurchase);
            return newPurchase;
        }

        public Purchase DeletePurchase(int id)
        {
            var purchaseToDelete = _purchaseRepository.GetAll().FirstOrDefault(p => p.PurchaseId == id);
            if (purchaseToDelete != null)
            {
                purchaseToDelete.IsVisible = false;
                _purchaseRepository.Update(purchaseToDelete);
            }

            return purchaseToDelete;
        }

        public List<GetPurchasesVM> GetByItemId(int itemId)
        {
            var purchasesToGet = _purchaseRepository.GetAll().Where(p => p.ItemId == itemId && p.IsVisible == true)
                .ToList();
            var allItems = _itemService.GetItems();
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
                    Price = item.purchase.Price,
                    IsVisible = item.purchase.IsVisible,
                    ItemName = item.Name,
                    Unit = item.Unit,
                    PurchasedBy = item.purchase.PurchasedBy
                };
                returnList.Add(newPurchase);
            }

            return returnList;
        }

        public List<Purchase> GetByItemIdNoVM(int itemId)
        {
            return _purchaseRepository.GetAll().Where(p => p.ItemId == itemId).ToList();
        }
    }
}