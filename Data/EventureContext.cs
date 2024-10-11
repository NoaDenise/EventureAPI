using EventureAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventureAPI.Data
{
    public class EventureContext : IdentityDbContext<User>
    {
        public EventureContext(DbContextOptions<EventureContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //indexing, making Email, Name and Location unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("Index_Email");


            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique()
                .HasDatabaseName("Index_CategoryName");


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(u => u.LastName)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(u => u.UserLocation);
            });

            modelBuilder.Entity<Attendance>()
            .HasOne(a => a.User)
            .WithMany(u => u.Attendances)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Event)
            .WithMany(e => e.Attendances)
            .HasForeignKey(a => a.EventId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
            .HasOne(c => c.Event)
            .WithMany(e => e.Comments)
            .HasForeignKey(c => c.EventId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
            .HasOne(r => r.Event)
            .WithMany(e => e.Ratings)
            .HasForeignKey(r => r.EventId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
