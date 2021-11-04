using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    public class BingoContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        
        public DbSet<Cluster> Clusters { get; set; }
        
        public DbSet<Table> Tables { get; set; }
        
        public DbSet<Reservation> Reservations { get; set; }
        
        public DbSet<Customer> Customers { get; set; }

        public BingoContext(DbContextOptions<BingoContext> contextOptions) : base(contextOptions)
        {
            
        }
    }
}