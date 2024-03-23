using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VehiclesFleet.Constants;
using VehiclesFleet.DataAccess.Entities;
using VehiclesFleet.Services.Contracts;

namespace VehiclesFleet.Services;

public class JwtService: IJwtService
{
    private IAppSettingsReader appSettingsReader;
    
    public JwtService(IAppSettingsReader appSettingsReader)
    {
        this.appSettingsReader = appSettingsReader;
    }
    
    public string GenerateToken(User existingUser, IList<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = GenerateClaims(existingUser, roles);
        var tokenDescriptor = GenerateTokenDescriptor(claims);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor GenerateTokenDescriptor(List<Claim> claims)
    {
        var key = Encoding.ASCII.GetBytes(appSettingsReader.GetValue(AppSettingsConstants.Section.Authorization,AppSettingsConstants.Keys.JwtSecretKey));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        return tokenDescriptor;
    }
    private static List<Claim> GenerateClaims(User existingUser, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, existingUser.Id),
            new(JwtRegisteredClaimNames.Email, existingUser.Email),
            new(ClaimTypes.Name, existingUser.Name)
               
        };
            
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }
}