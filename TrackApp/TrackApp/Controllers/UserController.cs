using Microsoft.AspNetCore.Mvc;
using TrackApp.Core.Authentication;
using TrackApp.Service;
using TrackApp.Service.ViewModels;

namespace TrackApp.Controllers;

[Route("[controller]/[action]")]
public class UserController:ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        var returnToken=_userService.Login(authenticationRequest);
        if (returnToken == null)
            return Unauthorized();
        return Ok(returnToken);
    }

    [HttpPost]
    public IActionResult Register([FromBody] RegisterUserVM userVm)
    {
        return Ok(_userService.Register(userVm));
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