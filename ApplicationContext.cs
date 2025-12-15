using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using demoNEW_EFCoreVersion.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace demoNEW_EFCoreVersion
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<UsersRole> UsersRoles { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<PickupPoint> PickupPoints { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderContents> OrderContents { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-17JT2LI\\SQLEXPRESS;Database=demoEFCore;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Явно указываем EF, что у Product PK - это Article
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Article);

            // Связь: Product.Article (PK) -> OrderContents.ProductId (FK)
            modelBuilder.Entity<OrderContents>()
                .HasOne(oc => oc.Product)
                .WithMany(p => p.OrderContents)
                .HasForeignKey(oc => oc.ProductId);

            // Связь: Order.Id (PK) -> OrderContents.OrderId (FK)
            modelBuilder.Entity<OrderContents>()
                .HasOne(oc => oc.Order)
                .WithMany(o => o.OrderContents)
                .HasForeignKey(oc => oc.OrderId);

            // Связь: UsersRole.Id (PK) -> Users.UsersRoleId (FK)
            modelBuilder.Entity<Users>()
                .HasOne(u => u.UsersRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.UsersRoleId);

            // Связь: OrderStatus.Id (PK) -> Order.OrderStatusId (FK)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderStatus)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.OrderStatusId);

            // Связь: PickupPoint.Id (PK) -> Order.PickupPointId (FK)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.PickupPoint)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PickupPointId);

            // Связь: Users.Id (PK) -> Order.UsersId (FK)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Users)
                .WithMany() // У Users нет ICollection<Order>, поэтому WithMany() пустой
                .HasForeignKey(o => o.UsersId);

            // Связи для Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Units)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UnitsId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Suppliers)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SuppliersId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manufacturers)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturersId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ProductCategoryId);
        }
    }
}