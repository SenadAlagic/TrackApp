using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core;

public class Repair
{
    [Key] 
    public int RepairId { get; set; }
    public int? ImageId { get; set; }
    public int? VideoId { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; } //enum later
    public DateTime? RepairArrival { get; set; }
    public bool Repaired { get; set; }
    public byte? Receipt { get; set; }
}