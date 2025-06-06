using DevTracker.Core.Application.InboundPorts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevTracker.Core.Application.Adapters;
public class LoginService : ILoginService
{
    private const string _jwtSettingsConstant = "JwtSettings";
    private const string _secretKeyConstant = "Key";
    private const string _issuerConstant = "Issuer";
    private const string _audienceConstant = "Audience";
    private const string _expirationInMinutesConstant = "ExpirationInMinutes";
    private const string _hardCodedRoleConstant = "User";
    private const string _hardCodedTestUserConstant = "testuser";
    private const string _hardCodedTestPasswordConstant = "testpassword";

    private readonly IConfiguration _configuration;
    public LoginService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(string username)
    {
        var jwtSettings = _configuration.GetSection(_jwtSettingsConstant);
        string? secretKey = jwtSettings[_secretKeyConstant];  
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new ArgumentNullException("Check Json Web Token generation settings");
        }
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        SigningCredentials signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, _hardCodedRoleConstant)
        };
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: jwtSettings[_issuerConstant],
            audience: jwtSettings[_audienceConstant],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings[_expirationInMinutesConstant])),
            signingCredentials: signature
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateLogin(string username, string password)
    {
        // Hardcoded validation for testing purposes
        if(username == _hardCodedTestUserConstant && password == _hardCodedTestPasswordConstant)
        {
            return true;
        }
        return false;
    }
}
