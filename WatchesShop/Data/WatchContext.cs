using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection;
using WatchesShop.Models;

namespace WatchesShop.Data
{
    public class WatchContext : DbContext
    {
        public WatchContext(DbContextOptions<WatchContext> options)
            : base(options)
        {
        }

        // DbSet Ч таблицы базы данных
        public DbSet<Watch> Watches { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<WatchColor> Colors { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Mechanism> Mechanisms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // —в€зи Watch с другими таблицами
            modelBuilder.Entity<Watch>()
                .HasOne(w => w.Gender)
                .WithMany()
                .HasForeignKey(w => w.GenderId);

            modelBuilder.Entity<Watch>()
                .HasOne(w => w.Brand)
                .WithMany()
                .HasForeignKey(w => w.BrandId);

            modelBuilder.Entity<Watch>()
                .HasOne(w => w.MechanismType)
                .WithMany()
                .HasForeignKey(w => w.MechanismTypeId);

            modelBuilder.Entity<Watch>()
                .HasOne(w => w.Style)
                .WithMany()
                .HasForeignKey(w => w.StyleId);

            modelBuilder.Entity<Watch>()
                .HasOne(w => w.Case)
                .WithMany()
                .HasForeignKey(w => w.CaseId);

            // Ќастройка Case
            modelBuilder.Entity<Case>()
                .HasOne(c => c.Material)
                .WithMany()
                .HasForeignKey(c => c.MaterialId);

            modelBuilder.Entity<Case>()
                .HasOne(c => c.Color)
                .WithMany()
                .HasForeignKey(c => c.ColorId);

            // “ипы decimal
            modelBuilder.Entity<Case>().Property(c => c.Diameter).HasColumnType("decimal(5,2)");
            modelBuilder.Entity<Case>().Property(c => c.LengthCase).HasColumnType("decimal(5,2)");
            modelBuilder.Entity<Case>().Property(c => c.Thickness).HasColumnType("decimal(5,2)");
            modelBuilder.Entity<Case>().Property(c => c.WaterResistance).HasColumnType("decimal(5,2)");
            modelBuilder.Entity<Case>().Property(c => c.Weight).HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Watch>().Property(w => w.Price).HasColumnType("decimal(10,2)");

            // Order Ч OrderItem св€зь
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Watch)
                .WithMany()
                .HasForeignKey(oi => oi.WatchId);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Watch>().HasData(
                new Watch
                {
                    WatchId = 1,
                    Model = "GA-24560R",
                    GenderId = 2,
                    BrandId = 17,
                    StyleId = 3,
                    MechanismTypeId = 1,
                    AssemblyFactory = "Netherlands",
                    CaseId = 13,
                    Price = 12340m,
                    ImagePath = "photo_watch/1.jpg"
                },
                new Watch
                {
                    WatchId = 2,
                    Model = "95707",
                    GenderId = 3,
                    BrandId = 9,
                    StyleId = 2,
                    MechanismTypeId = 2,
                    AssemblyFactory = "France",
                    CaseId = 8,
                    Price = 16700m,
                    ImagePath = "photo_watch/2.jpg"
                },
                new Watch
                {
                    WatchId = 3,
                    Model = "RH456",
                    GenderId = 1,
                    BrandId = 13,
                    StyleId = 3,
                    MechanismTypeId = 3,
                    AssemblyFactory = "South Korea",
                    CaseId = 9,
                    Price = 7689m,
                    ImagePath = "photo_watch/3.jpg"
                },
                 new Watch
                 {
                     WatchId = 4,
                     Model = "X100",
                     GenderId = 1,
                     BrandId = 5,
                     StyleId = 1,
                     MechanismTypeId = 2,
                     AssemblyFactory = "Japan",
                     CaseId = 2,
                     Price = 5400m,
                     ImagePath = "photo_watch/4.jpg"
                 },
    new Watch
    {
        WatchId = 5,
        Model = "Omega-2025",
        GenderId = 2,
        BrandId = 3,
        StyleId = 2,
        MechanismTypeId = 1,
        AssemblyFactory = "Switzerland",
        CaseId = 7,
        Price = 23500m,
        ImagePath = "photo_watch/5.jpg"
    },
    new Watch
    {
        WatchId = 6,
        Model = "A1DF6E",
        GenderId = 2,
        BrandId = 6,
        StyleId = 1,
        MechanismTypeId = 1,
        AssemblyFactory = "Netherlands",
        CaseId = 12,
        Price = 11110m,
        ImagePath = "photo_watch/6.jpg"
    },
    new Watch
    {
        WatchId = 7,
        Model = "GRT00-1A1ER",
        GenderId = 3,
        BrandId = 3,
        StyleId = 3,
        MechanismTypeId = 1,
        AssemblyFactory = "Thailand",
        CaseId = 13,
        Price = 12340m,
        ImagePath = "photo_watch/7.jpg"
    },
    new Watch
    {
        WatchId = 8,
        Model = "96A207",
        GenderId = 1,
        BrandId = 5,
        StyleId = 2,
        MechanismTypeId = 2,
        AssemblyFactory = "India",
        CaseId = 8,
        Price = 16700m,
        ImagePath = "photo_watch/8.jpg"
    },
    new Watch
    {
        WatchId = 9,
        Model = "96A215",
        GenderId = 1,
        BrandId = 5,
        StyleId = 3,
        MechanismTypeId = 1,
        AssemblyFactory = "Japan",
        CaseId = 9,
        Price = 7689m,
        ImagePath = "photo_watch/9.jpg"
    },
    new Watch
    {
        WatchId = 10,
        Model = "305SWBL",
        GenderId = 2,
        BrandId = 7,
        StyleId = 2,
        MechanismTypeId = 1,
        AssemblyFactory = "Switzerland",
        CaseId = 10,
        Price = 21360m,
        ImagePath = "photo_watch/10.jpg"
    },
    new Watch
    {
        WatchId = 11,
        Model = "334SBLBL-WORLD",
        GenderId = 2,
        BrandId = 7,
        StyleId = 1,
        MechanismTypeId = 1,
        AssemblyFactory = "China",
        CaseId = 11,
        Price = 18900m,
        ImagePath = "photo_watch/11.jpg"
    },
    new Watch
    {
        WatchId = 12,
        Model = "A168WG-9E",
        GenderId = 1,
        BrandId = 6,
        StyleId = 2,
        MechanismTypeId = 1,
        AssemblyFactory = "Switzerland",
        CaseId = 12,
        Price = 11110m,
        ImagePath = "photo_watch/12.jpg"
    },
    new Watch
    {
        WatchId = 13,
        Model = "GA-2100-1A1ER",
        GenderId = 1,
        BrandId = 6,
        StyleId = 3,
        MechanismTypeId = 1,
        AssemblyFactory = "Thailand",
        CaseId = 13,
        Price = 12340m,
        ImagePath = "photo_watch/13.jpg"
    }
            );
        }

    }
}
