using FoundationWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoundationWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Disaster> Disasters { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships and constraints if needed
            modelBuilder.Entity<User>()
                .HasMany(u => u.Volunteers)
                .WithOne(v => v.User)
                .HasForeignKey(v => v.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Resources)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Volunteer>()
                .HasMany(v => v.Assignments)
                .WithOne(a => a.Volunteer)
                .HasForeignKey(a => a.Volunteer_ID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Disaster>()
                .HasMany(d => d.Assignments)
                .WithOne(a => a.Disaster)
                .HasForeignKey(a => a.Disaster_ID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Disaster>()
                .HasMany(d => d.Donations)
                .WithOne(dn => dn.Disaster)
                .HasForeignKey(dn => dn.Disaster_ID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Resource>()
                .HasMany(r => r.Donations)
                .WithOne(dn => dn.Resource)
                .HasForeignKey(dn => dn.Resource_ID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
