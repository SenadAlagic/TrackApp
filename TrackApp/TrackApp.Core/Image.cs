using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core;

public class Image
{
    [Key]
    public int ImageId { get; set; }
    public byte[] ByteArray { set; get; }
}