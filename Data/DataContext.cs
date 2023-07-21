using E_cart.Models;
using Microsoft.EntityFrameworkCore;

namespace E_cart.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
