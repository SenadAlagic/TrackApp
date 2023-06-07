namespace TrackApp.Service.ViewModels;

public class AddRepairVM
{
    public int? ImageId { get; set; }
    public int? VideoId { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; } //enum later
}