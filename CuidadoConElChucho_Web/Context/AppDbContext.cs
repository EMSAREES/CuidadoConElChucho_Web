using Microsoft.EntityFrameworkCore;
using CuidadoConElChucho_Web.Entities;

namespace CuidadoConElChucho_Web.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(c => c.CategoryId);

                e.Property(c => c.CategoryId)
                    .ValueGeneratedOnAdd();

                e.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                e.HasData(
                    new Category { CategoryId = 1, Name = "Playeras" },
                    new Category { CategoryId = 2, Name = "Hoodies" },
                    new Category { CategoryId = 3, Name = "Pantalones" },
                    new Category { CategoryId = 4, Name = "Zapatos" },
                    new Category { CategoryId = 5, Name = "Accesorios" },
                    new Category { CategoryId = 6, Name = "Gorras" }
                );
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.HasKey(p => p.ProductId);

                e.Property(p => p.ProductId)
                    .ValueGeneratedOnAdd();

                e.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(p => p.Description)
                    .IsRequired();

                e.Property(p => p.Price)
                    .HasColumnType("decimal(10,2)");

                e.Property(p => p.Stock)
                    .IsRequired();

                // Category -> Products
                e.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<ProductVariation>(e =>
            {
                e.HasKey(pv => pv.ProductVariationId);

                e.Property(pv => pv.ProductVariationId)
                    .ValueGeneratedOnAdd();

                e.Property(pv => pv.Color)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(pv => pv.Size)
                    .IsRequired()
                    .HasMaxLength(10);

                e.Property(pv => pv.Stock)
                    .IsRequired();

                // Product -> Variations
                e.HasOne(pv => pv.Product)
                    .WithMany(p => p.Variations)
                    .HasForeignKey(pv => pv.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.UserId);

                e.Property(u => u.UserId)
                    .ValueGeneratedOnAdd();

                e.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(u => u.Password)
                    .IsRequired();

                e.Property(u => u.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                // Evita dos usuarios con el mismo correo
                e.HasIndex(u => u.Email)
                    .IsUnique();
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(o => o.OrderId);

                e.Property(o => o.OrderId)
                    .ValueGeneratedOnAdd();

                e.Property(o => o.OrderDate)
                    .IsRequired();

                e.Property(o => o.TotalAmount)
                    .HasColumnType("decimal(10,2)");

                // User -> Orders
                e.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderItem>(e =>
            {
                e.HasKey(oi => oi.OrderItemId);

                e.Property(oi => oi.OrderItemId)
                    .ValueGeneratedOnAdd();

                e.Property(oi => oi.Quantity)
                    .IsRequired();

                e.Property(oi => oi.Price)
                    .HasColumnType("decimal(10,2)");

                // Order -> OrderItems
                e.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Product -> OrderItems
                e.HasOne(oi => oi.product)
                    .WithMany()
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
