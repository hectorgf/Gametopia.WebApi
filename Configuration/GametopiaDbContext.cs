using Microsoft.EntityFrameworkCore;

namespace Gametopia.WebApi.Configuration
{
    public class GametopiaDbContext : DbContext
    {
        public GametopiaDbContext(DbContextOptions<GametopiaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración de relaciones y restricciones
        }
    }
}
