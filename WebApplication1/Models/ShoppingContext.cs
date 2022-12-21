using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ShoppingContext:DbContext
    {
        public ShoppingContext(DbContextOptions option) : base(option)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Global turn off delete behaviour on foreign keys
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<AddressDetail> AddressDetail { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
    }
}