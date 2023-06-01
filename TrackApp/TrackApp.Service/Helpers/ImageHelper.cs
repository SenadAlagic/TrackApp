namespace TrackApp.Helpers;

public class ImageHelper
{
    public byte[] ParseBase64(string base64string)
    {
        base64string = base64string.Split(',')[1];
        return System.Convert.FromBase64String(base64string);
    }
}