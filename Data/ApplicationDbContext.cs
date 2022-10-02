using Buy_And_Sell.Models;
using Microsoft.EntityFrameworkCore;

namespace Buy_And_Sell.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Buyer> Buyer { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<House> House { get; set; }
        public DbSet<SoldItems> SoldItems { get; set; }
    }
}
