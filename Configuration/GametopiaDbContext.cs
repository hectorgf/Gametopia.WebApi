using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gametopia.WebApi.Configuration
{
    public class GametopiaDbContext : IdentityDbContext<IdentityUser>
    {
        public GametopiaDbContext(DbContextOptions<GametopiaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración de relaciones y restricciones
        }
    }
}