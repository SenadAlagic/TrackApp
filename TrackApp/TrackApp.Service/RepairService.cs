using TrackApp.Core;
using TrackApp.Repository;
using TrackApp.Service.ViewModels;
namespace TrackApp.Service;

public interface IRepairService
{
    public Repair AddRepairRequest(AddRepairVM newRepair);
    public Repair GetById(int repairId);
    public List<Repair> GetAllRepairs();
}
public class RepairService : IRepairService
{
    private readonly IRepository<Repair> _repairRepository;

    public RepairService(IRepository<Repair> repairRepository)
    {
        this._repairRepository = repairRepository;
    }

    public Repair AddRepairRequest(AddRepairVM newRepair)
    {
        var repairToAdd = new Repair()
        {
            ImageId = newRepair.ImageId,
            VideoId = newRepair.VideoId,
            Description = newRepair.Description,
            Priority = newRepair.Priority,
            Repaired = false,
            RepairArrival = null,
            Receipt = null
        };
        _repairRepository.Add(repairToAdd);
        return repairToAdd;
    }

    public Repair GetById(int repairId)
    {
        return _repairRepository.GetAll().FirstOrDefault(r => r.RepairId == repairId);
    }

    public List<Repair> GetAllRepairs()
    {
        return _repairRepository.GetAll().ToList();
    }
}