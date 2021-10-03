using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FileHandler.Auth
{
  public class JwtAuthenticationService
  {
    private readonly string key;
    private readonly int expiresIn = Int32.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRES_IN_HOURS"));

    public JwtAuthenticationService (string key)
    {
      this.key = key;
    }

    public string Authenticate(string Email)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenKey = Encoding.ASCII.GetBytes(this.key);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.Email, Email)
          }),
        Expires = DateTime.UtcNow.AddHours(this.expiresIn),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
