using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Gametopia.WebApi.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtTokenForUser(IdentityUser user);
        string GenerateJwtTokenForService(string subject, string audience, TimeSpan? expiration = null, Claim[] additionalClaims = null);
    }
}