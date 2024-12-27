using Gametopia.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService : IJwtService
{
    private readonly string _key;
    private readonly string _issuer;

    public JwtService(IConfiguration configuration)
    {
        _key = configuration["JwtSettings:Key"] ?? throw new ArgumentNullException("JWT Key is not configured");
        _issuer = configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException("JWT Issuer is not configured");
    }

    public string GenerateJwtTokenForUser(IdentityUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var audience = "Gametopia.WebApi";

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddDays(1),  // Expiración del token (ajustable)
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateJwtTokenForService(string subject, string audience, TimeSpan? expiration = null, Claim[] additionalClaims = null)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        if (additionalClaims != null)
        {
            claims = CombineClaims(claims, additionalClaims);
        }

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(expiration ?? TimeSpan.FromMinutes(3)), // Valor predeterminado: 1 día
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private Claim[] CombineClaims(Claim[] baseClaims, Claim[] additionalClaims)
    {
        var combinedClaims = new Claim[baseClaims.Length + additionalClaims.Length];
        baseClaims.CopyTo(combinedClaims, 0);
        additionalClaims.CopyTo(combinedClaims, baseClaims.Length);
        return combinedClaims;
    }
}