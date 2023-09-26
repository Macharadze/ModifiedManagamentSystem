using Microsoft.EntityFrameworkCore;
using Modified.Models;

namespace Modified.Data
{
    public class MyDB : DbContext
    {
        public MyDB(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            {
                modelBuilder.Entity<User>()
       .HasOne(user => user.Profile)
       .WithOne(profile => profile.User)
       .HasForeignKey<UserProfile>(profile => profile.UserId);

                // Add other configurations if needed

                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
