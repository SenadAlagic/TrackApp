namespace TrackApp.Core.Authentication;

[Serializable]
public class JwtAuthResponse
{
    public string Token { get; set; }
    public string Username { get; set; }
    public int ExpiresIn { get; set; }
}