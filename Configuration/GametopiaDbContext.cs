using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gametopia.WebApi.Configuration
{
    public class GametopiaDbContext : IdentityDbContext<IdentityUser>
    {
        public GametopiaDbContext(DbContextOptions<GametopiaDbContext> options) : base(options) { }

        public DbSet<UserRelation> UserRelations { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Identity
            modelBuilder.Entity<IdentityUser>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<IdentityUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            // Configuración de UserRelation
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

            // Configuración de Profile
            modelBuilder.Entity<Profile>()
                .ToTable("Profiles");
            modelBuilder.Entity<Profile>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Profile>(p => p.Id); // Relación 1:1 con misma clave
        }
    }
}