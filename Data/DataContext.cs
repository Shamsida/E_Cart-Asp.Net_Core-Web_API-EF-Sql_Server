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
    }
}
