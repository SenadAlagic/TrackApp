using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TrackApp.Core.Authentication;

public static class JwtAuthenticationManager
{
    public static JwtAuthResponse Authenticate(string username, string password)
    {
        var tokenExpiryTimestamp = DateTime.Now.AddMinutes(30);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes("SomeSecretWeAreSupposedToHave");
        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
            {
                new Claim("username", username),
                new Claim(ClaimTypes.PrimaryGroupSid, "User Group 1")
            }),
            Expires = tokenExpiryTimestamp,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
        return new JwtAuthResponse()
        {
            Token = token,
            Username = username,
            ExpiresIn = (int)tokenExpiryTimestamp.Subtract(DateTime.Now).TotalSeconds
        };
    }
}