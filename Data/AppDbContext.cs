using Microsoft.EntityFrameworkCore;
using DamTunsi.Models;

namespace DamTunsi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USER
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.PhoneNumber).HasMaxLength(20);
                entity.Property(u => u.Role).HasConversion<int>();
                entity.Ignore(u => u.Age);
            });

            // DONOR
            modelBuilder.Entity<Donor>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.BloodType).IsRequired().HasMaxLength(5);
                entity.Property(d => d.Governorate).IsRequired().HasMaxLength(50);
                entity.Property(d => d.City).IsRequired().HasMaxLength(50);
                entity.HasOne(d => d.User)
                      .WithOne(u => u.Donor)
                      .HasForeignKey<Donor>(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // HOSPITAL
            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.HospitalName).IsRequired().HasMaxLength(150);
                entity.Property(h => h.Governorate).IsRequired().HasMaxLength(50);
                entity.Property(h => h.City).IsRequired().HasMaxLength(50);
                entity.Property(h => h.Status).HasConversion<int>();
                entity.Ignore(h => h.IsApproved);
                entity.HasOne(h => h.User)
                      .WithOne(u => u.Hospital)
                      .HasForeignKey<Hospital>(h => h.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // BLOOD REQUEST
            modelBuilder.Entity<BloodRequest>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.BloodType).IsRequired().HasMaxLength(5);
                entity.Property(r => r.UrgencyLevel).HasConversion<int>();
                entity.Property(r => r.Status).HasConversion<int>();
                entity.HasOne(r => r.Hospital)
                      .WithMany(h => h.Requests)
                      .HasForeignKey(r => r.HospitalId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // DONATION
            modelBuilder.Entity<Donation>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Status).HasConversion<int>();
                entity.HasOne(d => d.Donor)
                      .WithMany(d => d.Donations)
                      .HasForeignKey(d => d.DonorId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.Request)
                      .WithMany(r => r.Donations)
                      .HasForeignKey(d => d.RequestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // NOTIFICATION
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Message).IsRequired().HasMaxLength(500);
                entity.Property(n => n.Type).HasMaxLength(50);
                entity.Property(n => n.Channel).HasMaxLength(20);
                entity.HasOne(n => n.User)
                      .WithMany(u => u.Notifications)
                      .HasForeignKey(n => n.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}