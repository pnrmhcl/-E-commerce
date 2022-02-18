using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebUI.Models
{
    public partial class EnstrumanShopContext : DbContext
    {
        public EnstrumanShopContext()
        {
        }

        public EnstrumanShopContext(DbContextOptions<EnstrumanShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ContactMessage> ContactMessages { get; set; }
        public virtual DbSet<HomepageSlider> HomepageSliders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserBasket> UserBaskets { get; set; }
        public virtual DbSet<UserBasketItem> UserBasketItems { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB;initial catalog=EnstrumanShopv2;persist security info=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name).IsRequired();

                entity.HasData(
                    new Category() { Id=1, Name = "Nefesli" },
                    new Category() { Id=2, Name = "Vurmalı" },
                    new Category() { Id=3, Name = "Telli" },
                    new Category() { Id=4, Name = "Yaylı" },
                    new Category() { Id=5, Name = "Tuşlu" }
                    );
            });

            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<HomepageSlider>(entity =>
            {
                entity.Property(e => e.ButtonLink).IsRequired();

                entity.Property(e => e.ButtonText)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PicturePath).IsRequired();

                entity.Property(e => e.Text).IsRequired();

                entity.HasData(
                    new HomepageSlider() { Id=1,PicturePath = "girl1.jpg", ButtonText = "Alışverişe Başla!", ButtonLink = "/Shop/Index", Text = "<h3> %50'ye varan indirimleri kaçırma! </h3>" });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Orders")
                    .IsUnique();

                entity.Property(e => e.OrderCode).IsRequired();

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1)-(1))-(2021))");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_User");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Category");

                entity.HasData(
                    new Product() { Id = 1, Name = "Keman", Price = 250, UnitsInStock = 5, Description = "Keman açıklama texti.", CategoryId = 3 },
                    new Product() { Id = 2, Name = "Gitar", Price = 250, UnitsInStock = 5, Description = "Gitar açıklama texti.", CategoryId = 3 }
                    );
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.Property(e => e.Path).IsRequired();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImages_Products");

                entity.HasData(
                    new ProductImage() { Id=1, IsMainImg = false, Path = "keman.jpg", ProductId = 1 },
                    new ProductImage() { Id=2, IsMainImg = false, Path = "gitar.jpg", ProductId = 2 }
                    );
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Password).IsRequired();

                entity.HasData(
                    new User() { Id = 1, Email = "deneme@admin.com", Address = "Deneme adress", FirstName = "Admin", LastName = "Admin", Password = "202CB962AC59075B964B07152D234B70", },
                    new User() { Id = 2, Email = "deneme@musteri.com", Address = "Deneme adress", FirstName = "Müşteri", LastName = "Adı", Password = "202CB962AC59075B964B07152D234B70", }
                    );
            });

            modelBuilder.Entity<UserBasket>(entity =>
            {
                entity.ToTable("UserBasket");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBaskets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserBasket_User");

                entity.HasData(
                    new UserBasket() { Id=1,TotalPrice = 0, UserId = 1 },
                    new UserBasket() { Id=2,TotalPrice = 0, UserId = 2 }
                    );
            });

            modelBuilder.Entity<UserBasketItem>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Basket)
                    .WithMany(p => p.UserBasketItems)
                    .HasForeignKey(d => d.BasketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserBasketItems_UserBasket");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.UserBasketItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserBasketItems_Products");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.Role).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoles_User");

                entity.HasData(
                    new UserRole() {Id=1,Role = "admin", UserId = 1 }
                    );
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
