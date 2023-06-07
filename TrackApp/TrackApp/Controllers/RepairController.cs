using Microsoft.AspNetCore.Mvc;
using TrackApp.Core;
using TrackApp.Service;
using TrackApp.Service.ViewModels;

namespace TrackApp.Controllers;

[Route("[controller]/[action]")]
public class RepairController:ControllerBase
{
    private readonly IRepairService _repairService;

    public RepairController(IRepairService repairService)
    {
        _repairService = repairService;
    }

    [HttpGet]
    public ActionResult<Repair> GetRepairById(int repairId)
    {
        var repair = _repairService.GetById(repairId);
        if (repair == null)
            return BadRequest();
        return Ok(repair);
    }

    [HttpGet]
    public ActionResult<List<Repair>> GetAllRepairs()
    {
        var repairs = _repairService.GetAllRepairs();
        if (repairs == null)
            return BadRequest();
        return Ok(repairs);
    }

    [HttpPost]
    public ActionResult<Repair> AddRepair(AddRepairVM newRepair)
    {
        var repair = _repairService.AddRepairRequest(newRepair);
        return repair;
    }
}