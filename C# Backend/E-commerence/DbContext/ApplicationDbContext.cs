using E_commerence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_commerence.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Categroy> Categroies { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(u => u.Products)
                .HasForeignKey(o => o.CategoryId);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id)
                    .IsRequired();
            });
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(o => o.Id);
                e.Property(o => o.Id).IsRequired();
            });
            
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId);
            
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Product)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.ProductId);
            
            modelBuilder.Entity<OrderItem>(e =>
            {
                e.HasKey(o => o.Id);
                e.Property(o => o.Id).IsRequired();
            });
            
            modelBuilder.Entity<CartItem>()
                .HasOne(o => o.User)
                .WithMany(o => o.CartItems)
                .HasForeignKey(o => o.UserId);
            
            modelBuilder.Entity<CartItem>()
                .HasOne(o => o.Product)
                .WithMany(o => o.CartItems)
                .HasForeignKey(o => o.ProductId);
            
            modelBuilder.Entity<OrderItem>(e =>
            {
                e.HasKey(o => o.Id);
                e.Property(o => o.Id).IsRequired();
            });
        }
    }
}