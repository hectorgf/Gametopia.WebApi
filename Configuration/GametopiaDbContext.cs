using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gametopia.WebApi.Configuration
{
    public class GametopiaDbContext : IdentityDbContext<IdentityUser>
    {
        public GametopiaDbContext(DbContextOptions<GametopiaDbContext> options) : base(options) { }

        public DbSet<UserRelation> UserRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración de relaciones y restricciones

            modelBuilder.Entity<IdentityUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<UserRelation>()
                .ToTable("UserRelations");
            modelBuilder.Entity<UserRelation>()
                .HasKey(ur => ur.Id);
            modelBuilder.Entity<UserRelation>()
                .HasOne(ur => ur.SourceUser)
                .WithMany()
                .HasForeignKey(ur => ur.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRelation>()
                .HasOne(ur => ur.TargetUser)
                .WithMany()
                .HasForeignKey(ur => ur.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}