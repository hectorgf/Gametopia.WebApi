using Microsoft.AspNetCore.Identity;

public class Profile
{
    public string Id { get; set; }
    public string SteamUser { get; set; } = string.Empty;
    public string Nick { get; set; } = string.Empty;

    // Navegación a IdentityUser
    public IdentityUser User { get; set; }
}
