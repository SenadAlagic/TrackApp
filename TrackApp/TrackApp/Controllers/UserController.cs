using Microsoft.AspNetCore.Mvc;
using TrackApp.Service;

namespace TrackApp.Controllers;

[Route("[controller]/[action]")]
public class UserController:ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<Dictionary<string, int>> MostFrequentBuyers(int topX)
    {
        var returnList = _userService.MostFrequentBuyers(topX);
        if (returnList == null)
            return BadRequest();
        return Ok(returnList);
    }
    
    
    [HttpGet]
    public ActionResult<Dictionary<string, int>> MostFrequentRequests(int topX)
    {
        var returnList = _userService.MostFrequentRequest(topX);
        if (returnList == null)
            return BadRequest();
        return Ok(returnList);
    }
}