namespace Project.Models
{
    using Microsoft.EntityFrameworkCore;


    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        // Define DbSet properties for each entity/table
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships between entities

            // Example relationship configuration:
            // modelBuilder.Entity<Product>()
            //     .HasMany(p => p.Reviews)
            //     .WithOne(r => r.Product)
            //     .HasForeignKey(r => r.ProductId);
        }
    }
}
