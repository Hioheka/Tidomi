using Microsoft.EntityFrameworkCore;
using Tidomi.Domain.Entities;

namespace Tidomi.Infrastructure.Data;

public class TidomiDbContext : DbContext
{
    public TidomiDbContext(DbContextOptions<TidomiDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ServiceCategory> ServiceCategories { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<BookingItem> BookingItems { get; set; } = null!;
    public DbSet<Staff> Staff { get; set; } = null!;
    public DbSet<StaffSchedule> StaffSchedules { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        // ServiceCategory configuration
        modelBuilder.Entity<ServiceCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
            entity.Property(e => e.NameTr).IsRequired().HasMaxLength(200);
            entity.Property(e => e.DescriptionEn).HasMaxLength(1000);
            entity.Property(e => e.DescriptionTr).HasMaxLength(1000);
        });

        // Service configuration
        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
            entity.Property(e => e.NameTr).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(e => e.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Booking configuration
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.BookingNumber).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.BookingNumber).IsUnique();
            entity.Property(e => e.TotalAmount).HasPrecision(10, 2);

            entity.HasOne(e => e.Customer)
                .WithMany(u => u.Bookings)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.AssignedStaff)
                .WithMany(s => s.Bookings)
                .HasForeignKey(e => e.AssignedStaffId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // BookingItem configuration
        modelBuilder.Entity<BookingItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(10, 2);
            entity.Property(e => e.TotalPrice).HasPrecision(10, 2);

            entity.HasOne(e => e.Booking)
                .WithMany(b => b.BookingItems)
                .HasForeignKey(e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Service)
                .WithMany(s => s.BookingItems)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Staff configuration
        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Rating).HasPrecision(3, 2);

            entity.HasOne(e => e.User)
                .WithOne(u => u.Staff)
                .HasForeignKey<Staff>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // StaffSchedule configuration
        modelBuilder.Entity<StaffSchedule>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Staff)
                .WithMany(s => s.Schedules)
                .HasForeignKey(e => e.StaffId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Payment configuration
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasPrecision(10, 2);

            entity.HasOne(e => e.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed initial data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Service Categories
        var categoryId1 = Guid.NewGuid();
        var categoryId2 = Guid.NewGuid();
        var categoryId3 = Guid.NewGuid();
        var categoryId4 = Guid.NewGuid();

        modelBuilder.Entity<ServiceCategory>().HasData(
            new ServiceCategory
            {
                Id = categoryId1,
                NameEn = "Home Cleaning",
                NameTr = "Ev Temizliği",
                DescriptionEn = "Professional home cleaning services",
                DescriptionTr = "Profesyonel ev temizlik hizmetleri",
                DisplayOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new ServiceCategory
            {
                Id = categoryId2,
                NameEn = "Office Cleaning",
                NameTr = "Ofis Temizliği",
                DescriptionEn = "Professional office cleaning services",
                DescriptionTr = "Profesyonel ofis temizlik hizmetleri",
                DisplayOrder = 2,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new ServiceCategory
            {
                Id = categoryId3,
                NameEn = "Deep Cleaning",
                NameTr = "Derin Temizlik",
                DescriptionEn = "Thorough deep cleaning services",
                DescriptionTr = "Kapsamlı derin temizlik hizmetleri",
                DisplayOrder = 3,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new ServiceCategory
            {
                Id = categoryId4,
                NameEn = "Window Cleaning",
                NameTr = "Cam Silme",
                DescriptionEn = "Professional window cleaning",
                DescriptionTr = "Profesyonel cam temizleme",
                DisplayOrder = 4,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        // Seed Services
        modelBuilder.Entity<Service>().HasData(
            new Service
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId1,
                NameEn = "Standard Home Cleaning",
                NameTr = "Standart Ev Temizliği",
                DescriptionEn = "Regular home cleaning service",
                DescriptionTr = "Düzenli ev temizlik hizmeti",
                Type = ServiceType.Hourly,
                Price = 150,
                Unit = "hour",
                DurationMinutes = 60,
                DisplayOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Service
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId1,
                NameEn = "Premium Home Package",
                NameTr = "Premium Ev Paketi",
                DescriptionEn = "Complete home cleaning package",
                DescriptionTr = "Komple ev temizlik paketi",
                Type = ServiceType.Package,
                Price = 500,
                Unit = "session",
                DurationMinutes = 240,
                DisplayOrder = 2,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Service
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId2,
                NameEn = "Office Cleaning",
                NameTr = "Ofis Temizliği",
                DescriptionEn = "Professional office cleaning",
                DescriptionTr = "Profesyonel ofis temizliği",
                Type = ServiceType.PerArea,
                Price = 50,
                Unit = "sqm",
                DurationMinutes = 120,
                DisplayOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Service
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId3,
                NameEn = "Deep Cleaning Service",
                NameTr = "Derin Temizlik Hizmeti",
                DescriptionEn = "Comprehensive deep cleaning",
                DescriptionTr = "Kapsamlı derin temizlik",
                Type = ServiceType.Package,
                Price = 800,
                Unit = "session",
                DurationMinutes = 480,
                DisplayOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Service
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId4,
                NameEn = "Window Cleaning",
                NameTr = "Cam Temizleme",
                DescriptionEn = "Professional window cleaning",
                DescriptionTr = "Profesyonel cam temizleme",
                Type = ServiceType.Hourly,
                Price = 100,
                Unit = "hour",
                DurationMinutes = 60,
                DisplayOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
