using TrackApp.Core;
using TrackApp.Core.Authentication;
using TrackApp.Repository;
using TrackApp.Service.ViewModels;

namespace TrackApp.Service;

public interface IUserService
{
    public User Register(RegisterUserVM vm);
    public JwtAuthResponse Login(AuthenticationRequest authRequest);
    public Dictionary<string, int> MostFrequentBuyers(int topX);
    public Dictionary<string, int> MostFrequentRequest(int topX);
}

public class UserService : IUserService
{
    private readonly IItemListService _itemListService;
    private readonly IPurchaseService _purchaseService;
    private readonly IRepository<User> _userRepository;

    public UserService(IItemListService itemListService, IPurchaseService purchaseService,
        IRepository<User> userRepository)
    {
        _itemListService = itemListService;
        _purchaseService = purchaseService;
        _userRepository = userRepository;
    }


    public JwtAuthResponse Login(AuthenticationRequest authRequest)
    {
        var existingUser = _userRepository
            .GetAll().FirstOrDefault(u => u.Username == authRequest.Username && u.Password == authRequest.Password);
        if (existingUser == null)
            return null;
        var authResult = JwtAuthenticationManager.Authenticate(authRequest.Username, authRequest.Password);
        return authResult;
    }

    public User Register(RegisterUserVM userVm)
    {
        var newUser = new User()
        {
            FirstName = userVm.FirstName,
            LastName = userVm.LastName,
            Username = userVm.Username,
            Password = userVm.Password,
            Address = userVm.Address,
            Phone = userVm.Phone,
        };
        _userRepository.Add(newUser);
        return newUser;
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