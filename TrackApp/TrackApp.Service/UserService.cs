namespace TrackApp.Service;

public interface IUserService
{
    public Dictionary<string, int> MostFrequentBuyers(int topX);
    public Dictionary<string, int> MostFrequentRequest(int topX);
}

public class UserService : IUserService
{
    private readonly IItemListService _itemListService;
    private readonly IPurchaseService _purchaseService;

    public UserService(IItemListService itemListService, IPurchaseService purchaseService)
    {
        _itemListService = itemListService;
        _purchaseService = purchaseService;
    }

    public Dictionary<string, int> MostFrequentBuyers(int topX = 1)
    {
        var purchasesCount = _purchaseService.GetAll()
            .GroupBy(p => p.PurchasedBy)
            .Select(g => new { User = g.Key, Count = g.Count() });
        var mostFrequentBuyers = purchasesCount.OrderByDescending(p => p.Count).Take(topX);
        var returnDict = new Dictionary<string, int>();
        foreach (var buyer in mostFrequentBuyers)
        {
            returnDict.Add(buyer.User, buyer.Count);
        }

        return returnDict;
    }

    public Dictionary<string, int> MostFrequentRequest(int topX = 1)
    {
        var requestsCount = _itemListService.GetAllItemList().Where(il => il.CrossedOff == false)
            .GroupBy(il => il.AddedBy)
            .Select(g => new { User = g.Key, Count = g.Count() });
        var mostFrequentRequest = requestsCount.OrderByDescending(g => g.Count).Take(topX);
        var returnDict = new Dictionary<string, int>();
        foreach (var request in mostFrequentRequest)
        {
            returnDict.Add(request.User, request.Count);
        }

        return returnDict;
    }
}