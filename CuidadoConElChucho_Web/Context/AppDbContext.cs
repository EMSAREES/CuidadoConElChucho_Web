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
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // CATEGORY
            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(c => c.CategoryId);

                e.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                // No permite dos categorías con el mismo nombre
                e.HasIndex(c => c.Name)
                    .IsUnique();

                e.HasData(
                    new Category
                    {
                        CategoryId = 1,
                        Name = "PLAYERAS"
                    },
                    new Category
                    {
                        CategoryId = 2,
                        Name = "HOODIES"
                    },
                    new Category
                    {
                        CategoryId = 3,
                        Name = "PANTALONES"
                    },
                    new Category
                    {
                        CategoryId = 4,
                        Name = "ZAPATOS"
                    },
                    new Category
                    {
                        CategoryId = 5,
                        Name = "ACCESORIOS"
                    },
                    new Category
                    {
                        CategoryId = 6,
                        Name = "GORRAS"
                    }
                );
            });


            // COLOR
            modelBuilder.Entity<Color>(e =>
            {
                e.HasKey(c => c.ColorId);

                e.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(c => c.HexCode)
                    .HasMaxLength(7);

                // No permite dos colores con el mismo nombre
                e.HasIndex(c => c.Name)
                    .IsUnique();

                e.HasData(
                    new Color
                    {
                        ColorId = 1,
                        Name = "NEGRO",
                        HexCode = "#000000"
                    },
                    new Color
                    {
                        ColorId = 2,
                        Name = "BLANCO",
                        HexCode = "#FFFFFF"
                    },
                    new Color
                    {
                        ColorId = 3,
                        Name = "ROJO",
                        HexCode = "#FF0000"
                    },
                    new Color
                    {
                        ColorId = 4,
                        Name = "AZUL",
                        HexCode = "#0000FF"
                    },
                    new Color
                    {
                        ColorId = 5,
                        Name = "VERDE",
                        HexCode = "#008000"
                    },
                    new Color
                    {
                        ColorId = 6,
                        Name = "AMARILLO",
                        HexCode = "#FFFF00"
                    },
                    new Color
                    {
                        ColorId = 7,
                        Name = "NARANJA",
                        HexCode = "#FFA500"
                    },
                    new Color
                    {
                        ColorId = 8,
                        Name = "ROSA",
                        HexCode = "#FFC0CB"
                    },
                    new Color
                    {
                        ColorId = 9,
                        Name = "MORADO",
                        HexCode = "#800080"
                    },
                    new Color
                    {
                        ColorId = 10,
                        Name = "GRIS",
                        HexCode = "#808080"
                    },
                    new Color
                    {
                        ColorId = 11,
                        Name = "CAFE",
                        HexCode = "#A52A2A"
                    },
                    new Color
                    {
                        ColorId = 12,
                        Name = "BEIGE",
                        HexCode = "#F5F5DC"
                    }
                );
            });


            // SIZE
            modelBuilder.Entity<Size>(e =>
            {
                e.HasKey(s => s.SizeId);

                e.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                // No permite dos tallas iguales
                e.HasIndex(s => s.Name)
                    .IsUnique();

                e.HasKey(s => s.SizeId);

                e.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                e.HasIndex(s => s.Name)
                    .IsUnique();

                e.HasData(
                    new Size
                    {
                        SizeId = 1,
                        Name = "XS"
                    },
                    new Size
                    {
                        SizeId = 2,
                        Name = "S"
                    },
                    new Size
                    {
                        SizeId = 3,
                        Name = "M"
                    },
                    new Size
                    {
                        SizeId = 4,
                        Name = "L"
                    },
                    new Size
                    {
                        SizeId = 5,
                        Name = "XL"
                    },
                    new Size
                    {
                        SizeId = 6,
                        Name = "XXL"
                    },
                    new Size
                    {
                        SizeId = 7,
                        Name = "XXXL"
                    },
                    new Size
                    {
                        SizeId = 8,
                        Name = "28"
                    },
                    new Size
                    {
                        SizeId = 9,
                        Name = "30"
                    },
                    new Size
                    {
                        SizeId = 10,
                        Name = "32"
                    },
                    new Size
                    {
                        SizeId = 11,
                        Name = "34"
                    },
                    new Size
                    {
                        SizeId = 12,
                        Name = "36"
                    },
                    new Size
                    {
                        SizeId = 13,
                        Name = "38"
                    },
                    new Size
                    {
                        SizeId = 14,
                        Name = "40"
                    }
                );
            });


            // PRODUCT
            modelBuilder.Entity<Product>(e =>
            {
                e.HasKey(p => p.ProductId);

                e.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(p => p.Description)
                    .IsRequired();

                e.Property(p => p.Price)
                    .HasColumnType("decimal(10,2)");

                e.Property(p => p.IsActive)
                    .IsRequired();

                // Enum Gender se almacena como entero
                e.Property(p => p.Gender)
                    .IsRequired();


                // Category -> Products
                e.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // PRODUCT VARIATION
            modelBuilder.Entity<ProductVariation>(e =>
            {
                e.HasKey(pv => pv.ProductVariationId);

                e.Property(pv => pv.SKU)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(pv => pv.Stock)
                    .IsRequired();

                e.Property(pv => pv.ImageName)
                    .HasMaxLength(255);


                // SKU único
                e.HasIndex(pv => pv.SKU)
                    .IsUnique();


                // Un producto no puede tener dos veces
                // la misma combinación:
                //
                // PLAYERA + NARANJA + M
                //
                e.HasIndex(pv => new
                {
                    pv.ProductId,
                    pv.ColorId,
                    pv.SizeId
                })
                .IsUnique();


                // Product -> Variations
                e.HasOne(pv => pv.Product)
                    .WithMany(p => p.Variations)
                    .HasForeignKey(pv => pv.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);


                // Color -> Variations
                e.HasOne(pv => pv.Color)
                    .WithMany(c => c.ProductVariations)
                    .HasForeignKey(pv => pv.ColorId)
                    .OnDelete(DeleteBehavior.Restrict);


                // Size -> Variations
                e.HasOne(pv => pv.Size)
                    .WithMany(s => s.ProductVariations)
                    .HasForeignKey(pv => pv.SizeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // PRODUCT IMAGE
            modelBuilder.Entity<ProductImage>(e =>
            {
                e.HasKey(pi => pi.ProductImageId);

                e.Property(pi => pi.ImageName)
                    .IsRequired()
                    .HasMaxLength(255);

                e.Property(pi => pi.IsPrimary)
                    .IsRequired();


                // Product -> ProductImages
                e.HasOne(pi => pi.Product)
                    .WithMany()
                    .HasForeignKey(pi => pi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            // USER
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.UserId);

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


                // Un usuario no puede repetir correo
                e.HasIndex(u => u.Email)
                    .IsUnique();
            });


            // ADDRESS
            modelBuilder.Entity<Address>(e =>
            {
                e.HasKey(a => a.AddressId);

                e.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(a => a.ExteriorNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                e.Property(a => a.InteriorNumber)
                    .HasMaxLength(20);

                e.Property(a => a.Neighborhood)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(a => a.State)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(a => a.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10);


                // User -> Addresses
                e.HasOne(a => a.User)
                    .WithMany(u => u.Addresses)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            // ORDER
            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(o => o.OrderId);

                e.Property(o => o.OrderDate)
                    .IsRequired();

                e.Property(o => o.TotalAmount)
                    .HasColumnType("decimal(10,2)");

                e.Property(o => o.Status)
                    .IsRequired();


                // User -> Orders
                e.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });



            // ORDER ITEM
            modelBuilder.Entity<OrderItem>(e =>
            {
                e.HasKey(oi => oi.OrderItemId);

                e.Property(oi => oi.Quantity)
                    .IsRequired();

                e.Property(oi => oi.Price)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();


                // Order -> OrderItems
                e.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);


                // ProductVariation -> OrderItems
                e.HasOne(oi => oi.ProductVariation)
                    .WithMany()
                    .HasForeignKey(oi => oi.ProductVariationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
