namespace TrackApp.Core.Authentication;

[Serializable]
public class AuthenticationRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}