using Gametopia.WebApi.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

public class ProfileService
{
    readonly private GametopiaDbContext _dbContext;
    public ProfileService(GametopiaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Obtener datos de un usuario
    public async Task<Profile> GetProfileByIdAsync(string userId)
    {
        return _dbContext.Profiles.Include(x => x.User).Where(x => x.User.Id == userId).FirstOrDefault();
    }

    // Actualizar datos de usuario
    public async Task<Profile> UpdateProfileAsync(string userId, string steamUser, string nick)
    {
        var profile = _dbContext.Profiles.Include(x => x.User).Where(x => x.User.Id == userId).FirstOrDefault();

        if (profile != null)
        {
            profile.SteamUser = steamUser;
            profile.Nick = nick;

            _dbContext.Profiles.Update(profile);
        }
        else
        {
            profile = new Profile();

            profile.SteamUser = steamUser;
            profile.Nick = nick;
            profile.User = _dbContext.Users.Where(x => x.Id == userId).Single();

            _dbContext.Profiles.Add(profile);
        }

        _dbContext.SaveChanges();
        return profile;
    }
}